using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    public float timeToPassiveHeal = 5;
    public float passiveHealPerSecond = 1;
    public float value;
    public bool isDead;
    
    public float maxHp => _fish.stats.maxHp;
    public float percentage => value / maxHp;

    private float _lastDamage;
    private bool _isPassiveHealing;
    private BattleFish _fish = default!;
    
    public delegate void HealthChangeEvent(Health health, float amount);

    public event HealthChangeEvent? OnHealthGain;
    public event HealthChangeEvent? OnHealthLost;
    public event HealthChangeEvent? OnHealthDeath;

    private void Awake() {
        _fish = GetComponent<BattleFish>();
        Assert.IsNotNull(_fish);
    }

    private void Start()
    {
        _fish.onAttacked += OnAttacked;
        _fish.onHeal += OnHeal;
        
        value = maxHp;
    }

    private void Update()
    {
        if (CanPassiveHeal() && !_isPassiveHealing && !isDead)
            StartCoroutine(StartPassiveHeal());
    }

    private void OnDestroy()
    {
        _fish.onAttacked -= OnAttacked;       
        _fish.onHeal -= OnHeal;
    }

    private void OnAttacked(BattleFish target, BattleFish attacker)
    {
        float damage = attacker.stats.attack;
        if (Random.value < attacker.stats.criticalRate)
            damage *= attacker.stats.criticalMultiplier;
        
        TakeDamage(Math.Max(damage - target.stats.defense, 0));
    }

    private void OnHeal(BattleFish target, BattleFish healer)
    {
        float heal = healer.stats.attack;
        
        Heal(Math.Max(heal, 0));
    }

    private IEnumerator StartPassiveHeal()
    {
        _isPassiveHealing = true;
        while (CanPassiveHeal())
        {
            Heal(passiveHealPerSecond * Time.deltaTime);
            yield return null;
        }
        _isPassiveHealing = false;
    }

    private bool CanPassiveHeal()
    {
        return value < maxHp && _lastDamage + timeToPassiveHeal < Time.time;
    }
    
    public void TakeDamage(float amount)
    {
        value -= amount;
        value = Math.Max(value, 0);
        OnHealthLost?.Invoke(this, amount);

        if (value <= 0)
        {
            OnHealthDeath?.Invoke(this, amount);
            isDead = true;
        }

        _lastDamage = Time.time;
    }

    public void Heal(float amount)
    {
        value += amount;
        value = Math.Min(value, maxHp);
        OnHealthGain?.Invoke(this, amount);
    }
}