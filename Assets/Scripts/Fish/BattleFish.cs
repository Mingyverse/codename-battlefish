using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class BattleFish : MonoBehaviour
{
    public BattleFishData battleFishData = default!;
    [NonSerialized] public FishStats stats = default!;
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level = default!;
    [NonSerialized] public FishAI ai = default!;
    [NonSerialized] public FishAbility ability = default!;
    
    [NonSerialized] public Animator animator = default!;
    [NonSerialized] public Rigidbody2D rb = default!;
    
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
        
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);

        rb = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onAttacked?.Invoke(this, other.GetComponent<BattleFish>());
    }

    public delegate void AttackEvent(BattleFish target, BattleFish attacker);
    
    public AttackEvent? onAttacked;
    public AttackEvent? onHeal;
}