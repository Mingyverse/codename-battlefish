using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MeleeAI : FishAI
{
    private float _lockOnDistance = 4f;
    private bool _wentPastTarget;

    public override void Move()
    {
        GameObject? target = GetTarget();

        if (!target)
        {
            PassiveSwim();
        }
        else
        {
            #pragma warning disable
            Vector2 direction = target.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;
            
            if (lastTarget.x * direction.x > 0)  // have same sign
                _wentPastTarget = true;

            if (!_wentPastTarget || distanceSqr > _lockOnDistance * _lockOnDistance)
                lastTarget = direction;
            
            if (distanceSqr > _lockOnDistance)
                _wentPastTarget = false;

            #pragma warning restore

            float speed = 0.5f;
            if (!staminaFatigue)
            {
                bool goFaster = Random.Range(0, 6) == 0;
                
                speed = goFaster ? 2 : 1 * GetStaminaSpeed();
                ConsumeStamina(goFaster ? 1.2f : 1 * staminaUsePerSecond * Time.deltaTime);
            }

            fishRb.AddForce(lastTarget.normalized * speed);
        }
        
        fishRb.velocity = Vector2.ClampMagnitude(fishRb.velocity, maxSpeed);
    }
}