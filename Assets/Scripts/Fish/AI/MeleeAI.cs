using UnityEngine;

public class MeleeAI : FishAI
{
    public float lockOnDistance = 4f;
    
    public override void Move()
    {
        GameObject? target = GetTarget();
        #pragma warning disable
        if (lastAttacker && lastAttacker.health.isDead)
            lastAttacker = null;
        #pragma warning restore
        
        if (!target && !isPassive)
            target = lastAttacker?.gameObject;
        
        if (!target)
            PassiveSwim();
        else
        {
            #pragma warning disable
            Vector2 direction = target.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;
            #pragma warning restore
            
            if (distanceSqr > lockOnDistance * lockOnDistance)
                lastDirection = direction;
            
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