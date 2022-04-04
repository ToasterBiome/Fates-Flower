using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float direction = -4f;

    [SerializeField]
    Transform groundChecker;

    [SerializeField]
    Transform wallChecker;

    bool flip = true;

    public bool partrolling = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!partrolling) return;
        rb.velocity = new Vector2(direction * Time.deltaTime, rb.velocity.y + Physics2D.gravity.y * Time.deltaTime);
        if (flip)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (!Physics2D.OverlapCircle(groundChecker.position, 0.1f, 1 << LayerMask.NameToLayer("Ground")))
        {
            flip = true;
        }
        if (Physics2D.OverlapCircle(wallChecker.position, 0.1f, 1 << LayerMask.NameToLayer("Ground")))
        {
            flip = true;
        }
    }

    void Flip()
    {
        flip = false;
        transform.localScale = new Vector2(-transform.localScale.x, 1);
        direction = -direction;
    }
}
