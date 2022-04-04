using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    public bool activated = false;
    protected Rigidbody2D rb;

    [SerializeField]
    protected bool flip = true;

    [SerializeField]
    protected float direction = -1f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Flip()
    {
        flip = false;
        transform.localScale = new Vector2(-transform.localScale.x, 1);
        direction = -direction;
    }
}
