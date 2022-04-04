using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject destructionParticles;
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
        if (collider.tag == "Player")
        {
            WorldManager.instance.key = true;
            if (destructionParticles != null) Instantiate(destructionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
