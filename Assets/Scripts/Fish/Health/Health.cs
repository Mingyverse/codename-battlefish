using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Health : MonoBehaviour
{
    public float value;
    public float timeToPassiveHeal = 5;
    public float passiveHealPerSecond = 1;
    
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
        
        value = maxHp;
    }

    private void Update()
    {
        if (CanPassiveHeal() && !_isPassiveHealing)
            StartCoroutine(StartPassiveHeal());
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
            OnHealthDeath?.Invoke(this, amount);
        
        _lastDamage = Time.time;
    }

    public void Heal(float amount)
    {
        value += amount;
        value = Math.Min(value, maxHp);
        OnHealthGain?.Invoke(this, amount);
    }
}