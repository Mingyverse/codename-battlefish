using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class BattleFish : MonoBehaviour
{
    [FormerlySerializedAs("battleFishBase")] public BattleFishData battleFishData = default!;
    [NonSerialized] public FishStats stats = default!;
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level = default!;
    [NonSerialized] public FishAI ai = default!;
    [NonSerialized] public FishAbility ability = default!;
    
    private void Awake()
    {
        Assert.IsNotNull(battleFishData);
        
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