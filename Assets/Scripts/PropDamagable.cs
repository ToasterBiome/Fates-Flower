using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDamagable : MonoBehaviour, IDamageable
{
    [SerializeField] float health;

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float amount) { }
}