using System;
using UnityEngine;

public abstract class FishAI : MonoBehaviour
{
    public float speed = 1;
    
    protected Collider2D? target = null;
    protected float lastAttack = 0;
    
    protected BattleFish fish = default!;
    protected FishAbility ability = default!;
    protected Rigidbody2D rb = default!;
    protected ColliderObjects co = default!;

    public void Start()
    {
        fish = GetComponentInParent<BattleFish>();
        ability = fish.GetComponentInChildren<FishAbility>();
        rb = fish.GetComponent<Rigidbody2D>();
        co = GetComponent<ColliderObjects>();
    }

    public void Update()
    {
        if (CanAttack())
            Attack();
        
        if (CanMove())
            Move();
        
        if (ability.CanActivate())
            ability.Activate();
    }
    
    public bool CanMove()
    {
        return true;
    }

    public bool CanAttack()
    {
        return Time.time > lastAttack + fish.stats.attackSpeed;
    }

    public abstract void Move();
    public abstract void Attack();
    public abstract Collider2D? GetTarget();
}