using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public abstract class FishAI : MonoBehaviour
{
    public float maxSpeed = 10;
    public float maxStamina = 100; 
    public float stamina;

    public float staminaUsePerSecond = 20;
    public float staminaRestorePerSecond = 20;
    public float staminaRestoreDelay = 2;

    public float passiveSwimMinDelay = 0.2f;
    public float passiveSwimMaxDelay = 3f;
    
    protected bool staminaFatigue;
    protected float lastStaminaUse;
    protected bool isRestoringStamina;
    protected float waitUntilNextPassiveSwim;

    protected Vector2 lastTarget;
    protected bool isInCollision;
    protected BattleFish? lastAttacker;
    
    protected BattleFish fish = default!;
    protected FishAbility ability = default!;
    protected Rigidbody2D fishRb = default!;
    protected Animator animator = default!;
    protected SpriteRenderer spriteRenderer = default!;
    
    protected int animSwimming = Animator.StringToHash("Swimming");
    
    public void Awake()
    {
        stamina = maxStamina;
        
        ability = GetComponent<FishAbility>();
        Assert.IsNotNull(ability);
        
        fish = GetComponent<BattleFish>();
        Assert.IsNotNull(fish);
        
        fishRb = fish.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(fishRb);
        
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer);
    }

    private void FixedUpdate()
    {
        if (CanMove())
            Move();
    }

    private void Update()
    {
        if (CanRestoreStamina() && !isRestoringStamina)
            StartCoroutine(StartRestoreStamina());

        if (ability.CanActivate())
            ability.Activate();
        
        animator.SetBool(animSwimming, fishRb.velocity.sqrMagnitude > 1);
        
        if (fishRb.velocity.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (fishRb.velocity.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isInCollision = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isInCollision = false;
    }

    public float GetStaminaSpeed()
    {
        switch (stamina)
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
                return 2.1f + stamina / 1000.0f;
        }
    }
    
    protected void RestoreStamina(float amount)
    {
        stamina += amount;
        stamina = Math.Min(stamina, maxStamina);

        if (stamina >= maxStamina - 0.0001f)
            staminaFatigue = false;
    }

    protected void ConsumeStamina(float amount)
    {
        stamina -= amount;
        stamina = Math.Max(stamina, 0);
        
        lastStaminaUse = Time.time;

        if (stamina <= 0 + 0.0001f)
            staminaFatigue = true;
    }

    protected bool CanRestoreStamina()
    {
        return stamina < maxStamina && lastStaminaUse + staminaRestoreDelay < Time.time;
    }
    
    protected IEnumerator StartRestoreStamina()
    {
        isRestoringStamina = true;
        while (CanRestoreStamina())
        {
            RestoreStamina(staminaRestorePerSecond * Time.deltaTime);
            yield return null;
        }
        isRestoringStamina = false;
    }
    
    protected Vector2 GetRandomDirection()
    {
        float x = Random.Range(0, 2) == 1 ? 1 : -1 * Random.Range(1f, 2f);
        float y = Random.Range(0, 2) == 1 ? 1 : -1 * Random.Range(0.1f, 0.2f);

        return new Vector2(x, y);
    }

    protected void PassiveSwim()
    {
        if (Time.time < waitUntilNextPassiveSwim)
            return;

        if (isInCollision)
        {
            lastTarget = GetRandomDirection();
            lastTarget.y += -transform.position.y;
            lastTarget.x += -transform.position.x;
        }
        else
            lastTarget = GetRandomDirection();
        waitUntilNextPassiveSwim = Time.time + Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay);
            
        float speed = Random.Range(0.5f, 1.2f);
        fishRb.AddForce(lastTarget.normalized * (speed * Random.Range(passiveSwimMinDelay, passiveSwimMaxDelay)), ForceMode2D.Impulse);
    }

    public bool CanMove()
    {
        return true;
    }

    public abstract void Move();
    public abstract GameObject? GetTarget();
}