using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlying : AI
{
    Transform target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = WorldManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && target != null)
        {
            Vector2 direction = target.position - transform.position;
            rb.velocity = direction.normalized;
        }
    }
}
