using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float health = 10f * 60f; //10 minutes seconds of HP

    public static Action<float> OnHealthChanged;
    CharacterController controller;

    [SerializeField]
    SpriteRenderer sprite;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal") * 6f;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = 8;
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (velocity.x != 0)
        {
            sprite.flipX = Mathf.Sign(velocity.x) == 1 ? false : true;
        }

        //health time
        if (health > 0)
        {
            health -= Time.deltaTime;
            OnHealthChanged?.Invoke(health);
        }
    }


}
