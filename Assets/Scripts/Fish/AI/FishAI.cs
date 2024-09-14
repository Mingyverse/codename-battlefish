using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public abstract class FishAI : MonoBehaviour
{
    public float maxSpeed = 1;

    protected GameObject? cachedTarget; 
    protected float lastAttack = 0;
    protected GameObject[] targets = default!;
    
    protected BattleFish fish = default!;
    protected FishAbility ability = default!;
    protected Rigidbody2D rb = default!;
    protected CircleCollider2D col = default!;
    

    public void Start()
    {
        fish = GetComponentInParent<BattleFish>();
        ability = fish.GetComponentInChildren<FishAbility>();
        rb = fish.GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        if (fish.CompareTag("Team1"))
            targets = GameObject.FindGameObjectsWithTag("Team2");
        else
            targets = GameObject.FindGameObjectsWithTag("Team1");
        
        cachedTarget = GetTarget();
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
    public abstract GameObject? GetTarget();
}