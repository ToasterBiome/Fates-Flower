using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : AI
{

    [SerializeField]
    Transform groundChecker;

    [SerializeField]
    Transform wallChecker;

    // Update is called once per frame
    void Update()
    {
        if (!activated) return;
        rb.velocity = new Vector2(direction * Time.deltaTime * 4f, rb.velocity.y + Physics2D.gravity.y * Time.deltaTime);
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
}
