using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class FadeTo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer = default!;
    public float opacityPerSecond = 1.0f;
    public float opacity
    {
        get => spriteRenderer.color.a;
        set
        {
            Color color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        }
    }

    private IEnumerator? _lastFade;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        Assert.IsNotNull(spriteRenderer);
    }

    private void OnDestroy()
    {
        if (_lastFade != null)
            StopCoroutine(_lastFade);
    }
    
    private IEnumerator StartFading(float targetOpacity)
    {
        while (opacity < targetOpacity)
        {
            opacity += opacityPerSecond * Time.deltaTime;
            yield return null;
        }
        opacity = targetOpacity;
        
        _lastFade = null;
    }

    public void Cancel()
    {
        if (_lastFade != null)
            StopCoroutine(_lastFade);
    }
    
    public void Fade(float targetOpacity)
    {
        Cancel();
        
        _lastFade = StartFading(targetOpacity);
        StartCoroutine(_lastFade);
    }

    public void FadeIfNotAlready(float targetOpacity)
    {
        if (_lastFade == null)
            Fade(targetOpacity);
    }
}