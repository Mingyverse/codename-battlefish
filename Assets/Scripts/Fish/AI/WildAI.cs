using UnityEngine;

public class WildAI : FishAI
{
    public float fleeDistance = 3f;
    public float fishCalmAfterFlee = 3f;
    public float fleeDelayAfterNotProvoked = 0.1f;
    
    private float _lastProvoked = -10;
    private float _startFleeing;
    
    public override void Move()
    {
        GameObject? target = GetTarget();

        if (!target)
        {
            PassiveSwim();
            return;
        }

        #pragma warning disable
        Vector2 direction = target.transform.position - transform.position;
        float distanceSqr = direction.sqrMagnitude;
        #pragma warning restore

        if (distanceSqr > fleeDistance * fleeDistance)
        {
            if (IsCalm())
                PassiveSwim();
        }
        else
        {
            if (IsCalm() && _startFleeing < Time.time)
                if (_startFleeing < Time.time - fleeDelayAfterNotProvoked)
                {
                    _startFleeing = Time.time + fleeDelayAfterNotProvoked;
                    return;
                }

            if (_lastProvoked < Time.time - fleeDelayAfterNotProvoked)
            {
                fish.rb.AddForce(-direction.normalized, ForceMode2D.Impulse);
                ConsumeStamina(staminaUsePerSecond / 2);
            }
            

            _lastProvoked = Time.time;

            if (isInCollision)
            {
                direction.y += transform.position.y;
                direction.x += transform.position.x;
            }

            float speed = GetStaminaSpeed();
            if (!staminaFatigue)
            {
                bool goFaster = Random.Range(0, 6) == 0;
                
                speed *= goFaster ? 2 : 1;
                ConsumeStamina(goFaster ? 1.2f : 1 * staminaUsePerSecond * Time.deltaTime);
            }

            fish.rb.AddForce(-direction.normalized * speed);
        }
    }

    private bool IsCalm()
    {
        return Time.time > _lastProvoked + fishCalmAfterFlee;
    }
    
    public override GameObject? GetTarget()
    {
        return StageController.instance.player;
    }
}