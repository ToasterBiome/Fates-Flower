using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    float health = 20;


    [SerializeField]
    Animator animator;

    [SerializeField] string currentAnimState;

    [SerializeField] bool dying = false;

    [SerializeField] bool inCooldown = false;

    [SerializeField] SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            SetAnimationState("Walk");
        }
        else
        {
            SetAnimationState("Death");
        }
    }

    void SetAnimationState(string newState)
    {
        if (currentAnimState == newState) return;
        animator.Play(newState);
        currentAnimState = newState;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (dying) return;
        if (collider.tag == "Player")
        {
            IDamageable player = collider.GetComponent<IDamageable>();
            player.Damage(15f, true);
        }
    }
    IEnumerator DamageCooldown()
    {
        inCooldown = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
        inCooldown = false;
    }

    public void Damage(float amount, bool triggersCooldown)
    {
        if (triggersCooldown && inCooldown)
        {
            return;
        }
        else if (triggersCooldown)
        {
            StartCoroutine(DamageCooldown());
        }
        health -= amount;
        if (health <= 0 && !dying)
        {
            Die();
        }
    }

    void Die()
    {
        AIPatrol patrol = GetComponent<AIPatrol>();
        if (patrol != null)
        {
            patrol.partrolling = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        dying = true;
        Destroy(gameObject, 1f);
    }


    public void Heal(float amount, bool triggersCooldown)
    {
        throw new System.NotImplementedException();
    }
}
