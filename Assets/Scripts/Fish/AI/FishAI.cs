using UnityEngine;

namespace CodenameBattleFish;

public abstract class FishAI : MonoBehaviour
{
    
    private float _lastAct = 0;
    private BattleFish _fish;

    public FishAI()
    {
        _fish = GetComponent<BattleFish>();
    }

    public bool CanAct()
    {
        return Time.time > _lastAct + _fish.stats.attackSpeed;
    }
    
    public abstract void Act();
    public abstract GameObject GetTarget();
}