using UnityEngine;

public class ArcherfishAI : FishAI
{
    public float circlingDistance = 5;
    
    public override void Move()
    {
        GameObject? target = GetTarget();
        if (!target && !isPassive)
            target = lastAttacker?.gameObject;
        
        if (!target)
            PassiveSwim();
        else
        {
            #pragma warning disable
            Vector2 direction = target.transform.position - transform.position;
            #pragma warning restore
            
            if (direction.sqrMagnitude > circlingDistance * circlingDistance)
                lastDirection = direction;
            else
            {
                lastDirection = direction * (direction.magnitude - circlingDistance);
                lastDirection.y += transform.position.y;
                lastDirection.x += transform.position.x;
            }
            
            if (isInCollision)
            {
                lastDirection.y += -transform.position.y;
                lastDirection.x += -transform.position.x;
            }
            
            float speed = GetStaminaSpeed();
            if (!stamina.isFatigued)
            {
                bool goFaster = Random.Range(0, 6) == 0;
                
                speed *= goFaster ? 2 : 1;
                stamina.ConsumeStamina(goFaster ? 1.2f : 1 * staminaUsePerSecond);
            }

            fish.rb.AddForce(lastDirection.normalized * speed, ForceMode2D.Impulse);
            waitUntilNextSwim = Time.time + passiveSwimMinDelay;
        }
    }
}