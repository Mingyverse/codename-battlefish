using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float value { get; private set; }
    public float maxHp => _fish.stats.maxHp;
    public float percentage => value / maxHp;
    
    public delegate void HealthChangeEvent(Health health, float amount);
    public event HealthChangeEvent? OnHealthGain;
    public event HealthChangeEvent? OnHealthLost;
    public event HealthChangeEvent? OnHealthDeath;
    
    private BattleFish _fish = default!;

    private void Start()
    {
        _fish = GetComponentInParent<BattleFish>();
        value = maxHp;
    }

    public void TakeDamage(float amount)
    {
        value -= amount;
        value = Math.Max(value, 0);
        OnHealthLost?.Invoke(this, amount);
        
        if (value <= 0)
            OnHealthDeath?.Invoke(this, amount);
    }

    public void Heal(float amount)
    {
        value += amount;
        value = Math.Min(value, maxHp);
        OnHealthGain?.Invoke(this, amount);
    }
}