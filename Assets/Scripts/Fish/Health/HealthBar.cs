using System;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer healthSprite = default!;
    public FadeTo[] spriteFaders = Array.Empty<FadeTo>();
    
    private BattleFish _fish = default!;
    
    private void Awake()
    {
        _fish = GetComponentInParent<BattleFish>();
        Assert.IsNotNull(_fish);
        
        Assert.IsNotNull(healthSprite);

        if (spriteFaders.Length == 0)
            spriteFaders = GetComponentsInChildren<FadeTo>();
        Assert.IsFalse(spriteFaders.Length == 0);
    }

    private void Start()
    {
        foreach (FadeTo fadeTo in spriteFaders)
            fadeTo.opacity = 0;
                
        _fish.health.OnHealthGain += OnHealthChange;
        _fish.health.OnHealthGain += FadeHealthBar;
        _fish.health.OnHealthLost += OnHealthChange;
        _fish.health.OnHealthLost += AppearHealthBar;
    }

    private void OnHealthChange(Health health, float value)
    {
        Vector3 scale = healthSprite.transform.localScale;
        scale.x = health.percentage;
        
        healthSprite.transform.localScale = scale;
    }

    private void FadeHealthBar(Health health, float value)
    {
        if (health.percentage >= 0.99f && spriteFaders[0].opacity > 0.99f)
            foreach (FadeTo fadeTo in spriteFaders)
                fadeTo.FadeIfNotAlready(0);
    }

    private void AppearHealthBar(Health health, float value)
    {
        if (spriteFaders[0].opacity < 0.001f)
            foreach (FadeTo fadeTo in spriteFaders)
                fadeTo.FadeIfNotAlready(100);
    }

    private void OnDestroy()
    {
        _fish.health.OnHealthGain -= OnHealthChange;
        _fish.health.OnHealthGain -= FadeHealthBar;
        _fish.health.OnHealthLost -= OnHealthChange;
        _fish.health.OnHealthLost -= AppearHealthBar;
    }
}