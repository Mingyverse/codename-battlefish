using System;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleFish : MonoBehaviour
{
    public BattleFishBase battleFishBase = default!;
    [NonSerialized] public FishStats stats = default!;
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level = default!;
    [NonSerialized] public FishAI ai = default!;
    [NonSerialized] public FishAbility ability = default!;
    
    private void Awake()
    {
        Assert.IsNotNull(battleFishBase);
        
        level = new Level(this);   // TODO: save in persistence
        
        stats = FishStats.FromFish(this);  // initialize after level, depends on level
        
        health = GetComponent<Health>();  // initialize after stats, depends on stats
        Assert.IsNotNull(health);
        
        ability = GetComponent<FishAbility>();
        Assert.IsNotNull(ability);
        
        ai = GetComponent<FishAI>();
        Assert.IsNotNull(ai);
        
    }
}