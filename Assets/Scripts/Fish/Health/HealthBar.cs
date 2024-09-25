using System;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthBar : MonoBehaviour
{
    private BattleFish _fish = default!;
    private SpriteRenderer _sprite = default!;

    private void Awake()
    {
        _fish = GetComponentInParent<BattleFish>();
        Assert.IsNotNull(_fish);
        
        _sprite = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(_sprite);
    }

    private void Start()
    {
        _fish = GetComponentInParent<BattleFish>();
        _fish.health.OnHealthGain += OnHealthChange;
        _fish.health.OnHealthLost += OnHealthChange;
    }

    private void OnHealthChange(Health health, float value)
    {
        Vector3 scale = transform.localScale;
        scale.x = health.percentage;
        
        transform.localScale = scale;
    }

    private void OnDestroy()
    {
        _fish.health.OnHealthGain -= OnHealthChange;
        _fish.health.OnHealthLost -= OnHealthChange;
    }
}