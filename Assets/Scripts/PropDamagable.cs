using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDamagable : MonoBehaviour, IDamageable
{
    [SerializeField] float health;
    [SerializeField] GameObject destructionParticles;
    [SerializeField] Vector3 particlesOffset;

    public void Damage(float amount, bool triggersCooldown)
    {
        health -= amount;
        if (health <= 0)
        {
            if (destructionParticles != null) Instantiate(destructionParticles, transform.position + particlesOffset, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Heal(float amount, bool triggersCooldown) { }
}
