using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MeleeAI : FishAI
{
    private Vector2 _lastTarget;
    private float _lockOnDistance = 4f;
    private float _waitUntilSwim;
    private bool _wentPastTarget;

    public float passiveSwimMinDelay = 0.2f;
    public float passiveSwimMaxDelay = 3f;

    private bool randomBool => Random.Range(0, 2) == 1;
    
    private Vector2 GetRandomDirection()
    {
        float x = randomBool ? 1 : -1 * Random.Range(0.8f, 1.6f);
        float y = randomBool ? 1 : -1 * Random.Range(0.1f, 0.2f);

        return new Vector2(x, y);
    }

    public override void Move()
    {
        GameObject? target = GetTarget();

        if (!target)
        {
            if (Time.time < _waitUntilSwim)
                return;
            
            _lastTarget = GetRandomDirection();
            _waitUntilSwim = Time.time + Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay);
            
            float speed = GetStaminaSpeed();
            fishRb.AddForce(_lastTarget.normalized * (speed * Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay)), ForceMode2D.Impulse);
        }
        else
        {
            #pragma warning disable
            Vector2 direction = target.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;
            
            if (_lastTarget.x * direction.x > 0)  // have same sign
                _wentPastTarget = true;

            if (!_wentPastTarget || distanceSqr > _lockOnDistance * _lockOnDistance)
                _lastTarget = direction;
            
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

            fishRb.AddForce(_lastTarget.normalized * speed);
        }
        
        fishRb.velocity = Vector2.ClampMagnitude(fishRb.velocity, maxSpeed);
    }

    public override GameObject? GetTarget()
    {
        if (lastAttacker)
            return StageController.instance.GetClosestFish(transform, battleFish => !battleFish.CompareTag(tag))?.gameObject;
        return null;
    }
}