using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class FishAI : MonoBehaviour
{
    public float maxSpeed = 10;
    public float maxStamina = 100; 
    public float stamina;

    public float staminaUsePerSecond = 10;
    public float staminaRestorePerSecond = 20;
    public float staminaRestoreDelay = 2;

    protected bool staminaFatigue;
    protected float lastStaminaUse;
    protected bool isRestoringStamina;

    protected BattleFish? lastAttacker;
    
    protected BattleFish fish = default!;
    protected FishAbility ability = default!;
    protected Rigidbody2D fishRb = default!;
    
    public void Awake()
    {
        stamina = maxStamina;
        
        ability = GetComponent<FishAbility>();
        Assert.IsNotNull(ability);
        
        fish = GetComponent<BattleFish>();
        Assert.IsNotNull(fish);
        
        fishRb = fish.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(fishRb);
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
    }

    public float GetStaminaSpeed()
    {
        switch (stamina)
        {
            case <= 0:
                return 0.4f;
            case <= 20:
                return 0.6f;
            case <= 60:
                return 1f;
            case <= 100:
                return 1.5f;
            default:
                return 1.6f + stamina / 1000.0f;
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

    public bool CanMove()
    {
        return true;
    }

    public abstract void Move();
    public abstract GameObject? GetTarget();
}