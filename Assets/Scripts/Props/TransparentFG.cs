using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class TransparentFG : MonoBehaviour
{
    public float opacityPerSecond = 1.0f;
    
    private SpriteRenderer _sprite = default!;
    private IEnumerator? _lastFade;
    
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(_sprite);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActiveAndEnabled)
        {
            StopLastFade();
            
            _lastFade = FadeTo(0.7f);
            StartCoroutine(_lastFade);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActiveAndEnabled)
        {
            StopLastFade();
            
            _lastFade = FadeTo(1.0f);
            StartCoroutine(_lastFade);
        }
    }
    
    IEnumerator FadeTo(float targetOpacity)
    {
        float duration = CalculateDuration(targetOpacity);
        float elapsedTime = 0.0f;
        Color color;
        
        do
        {
            color = _sprite.color;
            color.a = Mathf.Lerp(_sprite.color.a, targetOpacity, Math.Min(elapsedTime / duration, 1.0f));
            _sprite.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        } while (elapsedTime < duration);

        // always set targetOpacity in case of elapsedTime > duration
        color.a = targetOpacity;
        _sprite.color = color;
    }

    private float CalculateDuration(float targetOpacity)
    {
        return Math.Abs(_sprite.color.a - targetOpacity) / opacityPerSecond;
    }

    private void StopLastFade()
    {
        if (_lastFade != null)
            StopCoroutine(_lastFade);
    }
}
