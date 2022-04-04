using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooter : AI
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float lastShot;
    [SerializeField] float shotCooldown = 3f;

    [SerializeField]
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
        if (!activated)
        {
            return;
        }
        Vector2 targetDirection = (target.position - transform.position).normalized;
        if (targetDirection.x > 0 && direction < 0 || targetDirection.x < 0 && direction > 0)
        {
            flip = true;
        }
        if (flip)
        {
            Flip();
        }
        if (Time.time >= lastShot + shotCooldown)
        {
            Shoot(targetDirection);
        }
    }

    void Shoot(Vector2 direction)
    {
        lastShot = Time.time;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Initialize(direction);
    }
}
