using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public AudioSource collectSound;
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
            PlayerController player = collider.GetComponent<PlayerController>();
            player.Heal(60, false); //1 minute yippoe!!!
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position, 2f);
            Destroy(gameObject);
        }
    }
}
