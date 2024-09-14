using System.Linq;
using UnityEngine;

public class MeleeAI : FishAI
{
    public float fleeThreshold = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        cachedTarget = GetTarget();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        cachedTarget = GetTarget();
    }

    public override void Move()
    {
        Vector2 swimDirection = Vector2.right;
        if (cachedTarget != null)
            swimDirection = cachedTarget.transform.position - transform.position;
        else
        {
            Debug.Log("No Target");
        }
        

        if (rb.velocity.y < -0.5)
            swimDirection.y += 2;
        
        if (fish.health.percentage < fleeThreshold)
            swimDirection = -swimDirection;
        
        rb.AddForce(swimDirection.normalized);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public override void Attack()
    {
        
    }

    public override GameObject? GetTarget()
    {
        return targets
            .Where(hitCol => hitCol.GetComponent<BattleFish>())
            .Where(hitCol => Vector2.Distance(transform.position, hitCol.transform.position) < col.radius)
            .OrderByDescending(hitCol => Vector2.Distance(transform.position, hitCol.transform.position))
            .FirstOrDefault();
    }
}