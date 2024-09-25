using System;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class FishAbility : MonoBehaviour
{
    public string abilityName = "";
    public float cooldownDuration;

    protected BattleFish fish = default!;
    protected float lastProc = 0;

    private void Awake()
    {
        fish = GetComponent<BattleFish>();
        Assert.IsNotNull(fish);
    }

    public bool CanActivate()
    {
        return Time.time > lastProc + cooldownDuration;
    }
    
    public abstract void Activate();
}