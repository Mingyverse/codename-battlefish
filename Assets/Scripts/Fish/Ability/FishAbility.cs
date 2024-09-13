using UnityEngine;

public abstract class FishAbility : MonoBehaviour
{
    public string abilityName = default!;
    public float cooldownDuration;
    
    private float _lastProc = 0;
    
    public bool CanActivate()
    {
        return Time.time > _lastProc + cooldownDuration;
    }
    
    public abstract void Activate();
}