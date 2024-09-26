using System;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer sprite = default!;
    
    private BattleFish _fish = default!;

    private void Awake()
    {
        _fish = GetComponentInParent<BattleFish>();
        Assert.IsNotNull(_fish);
        
        Assert.IsNotNull(sprite);
    }

    private void Start()
    {
        _fish = GetComponentInParent<BattleFish>();
        _fish.health.OnHealthGain += OnHealthChange;
        _fish.health.OnHealthLost += OnHealthChange;
    }

    private void OnHealthChange(Health health, float value)
    {
        Vector3 scale = sprite.transform.localScale;
        scale.x = health.percentage;
        
        sprite.transform.localScale = scale;
    }

    private void OnDestroy()
    {
        _fish.health.OnHealthGain -= OnHealthChange;
        _fish.health.OnHealthLost -= OnHealthChange;
    }
}