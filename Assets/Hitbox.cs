using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable == null)
        {
            return;
        }
        damageable.Damage(10f);
    }
}
