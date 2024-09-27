using UnityEngine;

public class Archery : FishAbility
{
    public override void Activate()
    {
        GameObject? target = fish.ai.GetTarget();
        if (!target && !fish.ai.isPassive)
            target = fish.ai.lastAttacker?.gameObject;

        if (!target) return;
        
        
        lastProc = Time.time;
        fish.ai.lastAttackedTime = Time.time;
        
        #pragma warning disable
        Vector2 direction = target.transform.position - transform.position;
        #pragma warning restore
        
        fish.rb.velocity = Vector2.ClampMagnitude(direction, 0.1f);
        
        Debug.Log("Shooting ");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        BattleFish? enemy = hit.rigidbody?.GetComponent<BattleFish>();
        enemy?.onAttacked?.Invoke(enemy, fish);
    }
}