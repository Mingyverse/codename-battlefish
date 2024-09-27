using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public abstract class FishAI : MonoBehaviour
{
    public Stamina stamina = default!;
    public float staminaUsePerSecond = 20;
    
    public float passiveSwimMinDelay = 0.2f;
    public float passiveSwimMaxDelay = 3f;
    protected float waitUntilNextSwim;
    protected bool isInCollision;

    public bool autoPassive = true;
    protected bool isPassive;
    public float passiveDelay = 2f;

    public bool isPlayer = true;
        
    protected float lastAttackedTime;
    protected BattleFish? lastAttacker;
    
    protected BattleFish fish = default!;

    protected Vector2 lastDirection;
    
    protected int animSwimming = Animator.StringToHash("Swimming");
    
    private void Awake()
    {
        fish = GetComponent<BattleFish>();
        Assert.IsNotNull(fish);
     
        if (stamina == null) stamina = GetComponent<Stamina>();
        Assert.IsNotNull(stamina);
    }

    private void Start()
    {
        fish.onAttacked += OnAttacked;
    }

    private void OnDestroy()
    {
        fish.onAttacked -= OnAttacked;
    }

    private void FixedUpdate()
    {
        if (!CanAct())
            return;
        
        if (CanMove())
            Move();
    }

    private void Update()
    {
        if (!CanAct())
            return;
        
        if (fish.ability.CanActivate() && !isPassive)
            fish.ability.Activate();
        
        if (autoPassive && Time.time > lastAttackedTime + passiveDelay)
            isPassive = true;
        
        fish.animator.SetBool(animSwimming, fish.rb.velocity.sqrMagnitude > 1);
        
        if (fish.rb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (fish.rb.velocity.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isInCollision = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isInCollision = false;
    }

    protected bool CanAct()
    {
        return !fish.health.isDead;
    }
    
    public float GetStaminaSpeed()
    {
        switch (stamina.value)
        {
            case <= 0:
                return 0.4f;
            case <= 20:
                return 0.7f;
            case <= 60:
                return 1f;
            case <= 100:
                return 2f;
            default:
                return 2.1f + stamina.value / 1000.0f;
        }
    }
    
    protected Vector2 GetRandomDirection()
    {
        float x = Random.Range(1f, 2f);
        float y = Random.Range(0.1f, 0.2f);
        
        if (!isPlayer)
        {
            x = Random.Range(0, 2) == 1 ? 1 : -1 * x;
            y = Random.Range(0, 2) == 1 ? 1 : -1 * y;
        }

        return new Vector2(x, y);
    }

    protected void PassiveSwim()
    {
        if (Time.time < waitUntilNextSwim)
            return;

        if (isInCollision)
        {
            lastDirection = GetRandomDirection();
            lastDirection.y += -transform.position.y;
            lastDirection.x += -transform.position.x;
        }
        else
            lastDirection = GetRandomDirection();
        waitUntilNextSwim = Time.time + Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay);
            
        float speed = Random.Range(0.5f, 1.2f);
        fish.rb.AddForce(lastDirection.normalized * (speed * Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay)), ForceMode2D.Impulse);
    }

    public bool CanMove()
    {
        return Time.time > waitUntilNextSwim;
    }

    private void OnAttacked(BattleFish target, BattleFish attacker)
    {
        isPassive = false;
        lastAttacker = attacker;
        lastAttackedTime = Time.time;
    }

    public GameObject? GetTarget()
    {
        return StageController.instance.GetClosestFish(transform, battleFish => !battleFish.CompareTag(tag) && !battleFish.health.isDead, fish.stats.range)?.gameObject;
    }
    
    public abstract void Move();
}