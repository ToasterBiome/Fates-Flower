using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public AudioSource enemySpawn;
    public AudioSource enemyDeath;


    [SerializeField]
    float health = 20;


    [SerializeField]
    Animator animator;

    [SerializeField] string currentAnimState;

    [SerializeField] bool dying = false;
    [SerializeField] bool spawning = false;

    [SerializeField] bool inCooldown = false;

    [SerializeField] SpriteRenderer sprite;

    public Action<GameObject> OnDeath;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            SetAnimationState("Spawn");
            return;
        }
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

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
        AudioSource.PlayClipAtPoint(enemySpawn.clip, transform.position, 2f);
    }

    IEnumerator SpawnCoroutine()
    {
        spawning = true;
        yield return new WaitForSeconds(1);
        spawning = false;
        AI ai = GetComponent<AI>();
        if (ai != null)
        {
            ai.activated = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        OnDeath?.Invoke(gameObject);
        AI ai = GetComponent<AI>();
        if (ai != null)
        {
            ai.activated = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        dying = true;
        Destroy(gameObject, 1f);
        AudioSource.PlayClipAtPoint(enemyDeath.clip, transform.position, 2f);
    }


    public void Heal(float amount, bool triggersCooldown)
    {
        throw new System.NotImplementedException();
    }
}
