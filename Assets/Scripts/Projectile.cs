using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction * 4;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy") return;
        if (collider.tag == "Platform") return;
        if (collider.tag == "Player")
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable.Damage(10f, true);

        }
        Debug.Log(collider.tag);
        Destroy(gameObject);
    }
}
