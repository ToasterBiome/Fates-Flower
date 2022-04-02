using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float health = 10f * 60f; //10 minutes seconds of HP

    public static Action<float> OnHealthChanged;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField] bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Vector2 velocity = rb.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * 6f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = 10;
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        //velocity.y = Mathf.Min(Physics2D.gravity.y, velocity.y);
        rb.velocity = velocity;

        if (rb.velocity.x != 0)
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

    bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, 1 << LayerMask.NameToLayer("Ground"));
        if (raycast.collider == null)
        {
            isGrounded = false;
            return false;
        }
        Debug.Log(raycast.collider.name);
        isGrounded = true;
        return true;
    }


}
