using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField]
    float health = 10f * 60f; //10 minutes seconds of HP

    public static Action<float> OnHealthChanged;
    public static Action OnDeath;
    public static Action OnDamage;
    public static Action OnHeal;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField] bool isGrounded = false;

    [SerializeField] GameObject platform;

    [SerializeField] Animator animator;
    [SerializeField] string currentAnimState;

    [SerializeField] bool attacking = false;
    [SerializeField] GameObject attackHitboxR;
    [SerializeField] GameObject attackHitboxL;

    [SerializeField] Coroutine platformCoroutine;

    [SerializeField] bool inCooldown = false;

    [SerializeField] bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Vector2 velocity = rb.velocity;
        if (!dead) velocity.x = Input.GetAxisRaw("Horizontal") * 6f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !dead)
        {
            velocity.y = 10.5f;
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        //velocity.y = Mathf.Min(Physics2D.gravity.y, velocity.y);
        rb.velocity = velocity;

        if (rb.velocity.x != 0)
        {
            sprite.flipX = Mathf.Sign(velocity.x) == 1 ? false : true;
        }

        Damage(Time.deltaTime, false);
        if (Input.GetKey(KeyCode.S) && platform != null && platformCoroutine == null)
        {
            platformCoroutine = StartCoroutine(DisablePlatform());
        }

        bool moving = false;
        if (velocity.x != 0)
        {
            moving = true;
        }


        if (Input.GetMouseButtonDown(0) && !attacking && !dead)
        {
            StartCoroutine(DoAttack());
        }

        string nextAnimationState = "Idle";

        if (!moving && isGrounded) nextAnimationState = "Idle";
        if (moving && isGrounded) nextAnimationState = "Run";
        if (!moving && !isGrounded) nextAnimationState = "Jump";
        if (moving && !isGrounded) nextAnimationState = "Jump";

        if (attacking)
        {
            nextAnimationState += "_Attack" + (sprite.flipX == true ? "_L" : "_R");
        }

        if (dead)
        {
            nextAnimationState = "Death";
        }

        SetAnimationState(nextAnimationState);
    }

    bool GroundCheck()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, 1 << LayerMask.NameToLayer("Ground"));
        if (raycast.collider == null)
        {
            isGrounded = false;
            return false;
        }
        isGrounded = true;
        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            platform = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && collision.gameObject == platform)
        {
            platform = null;
        }
    }

    IEnumerator DisablePlatform()
    {
        BoxCollider2D platformCollider = platform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), platformCollider, false);
        platformCoroutine = null;
    }

    IEnumerator DoAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.33f);
        if (!sprite.flipX)
        {
            attackHitboxR.SetActive(true);
        }
        else
        {
            attackHitboxL.SetActive(true);
        }

        yield return new WaitForSeconds(0.165f);
        attackHitboxR.SetActive(false);
        attackHitboxL.SetActive(false);
        attacking = false;
    }

    IEnumerator DamageCooldown()
    {
        inCooldown = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(1);
        sprite.color = Color.white;
        inCooldown = false;
    }

    public void Damage(float amount, bool triggersCooldown)
    {
        if (dead) return; //cant get deader bro!
        if (triggersCooldown && inCooldown)
        {
            return;
        }
        else if (triggersCooldown)
        {
            StartCoroutine(DamageCooldown());
            OnDamage?.Invoke();
        }
        health -= amount;
        OnHealthChanged?.Invoke(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        OnDeath?.Invoke();
    }

    public void Heal(float amount, bool triggersCooldown)
    {
        health += amount;
        OnHealthChanged?.Invoke(health);
        OnHeal?.Invoke();
    }

    void SetAnimationState(string newState)
    {
        if (currentAnimState == newState) return;
        animator.Play(newState);
        currentAnimState = newState;
    }
}
