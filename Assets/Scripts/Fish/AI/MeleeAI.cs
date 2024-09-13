using System;
using System.Linq;
using UnityEngine;

public class MeleeAI : FishAI
{
    public float fleeThreshold = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        target = GetTarget();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        target = GetTarget();
    }

    public override void Move()
    {
        
        Vector2 swimTo = Vector2.right;

        if (rb.velocity.y < -0.5)
            swimTo.y += 2;
        
        if (target != null)
            swimTo = target.transform.position;
        
        if (fish.health.value / fish.stats.maxHp < fleeThreshold)
            swimTo = -swimTo;

        
        Vector2 pos = Vector2.MoveTowards(transform.position, swimTo, 50 * speed * Time.deltaTime);
        
        rb.AddForce(pos - (Vector2)transform.position);
    }

    public override void Attack()
    {
        
    }

    public override Collider2D? GetTarget()
    {
        return co.colliders
            .Where(col => col.GetComponent<BattleFish>())
            .OrderByDescending(col => Vector2.Distance(transform.position, col.transform.position))
            .FirstOrDefault();
    }
}