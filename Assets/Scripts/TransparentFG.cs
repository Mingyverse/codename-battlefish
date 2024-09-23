using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class TransparentFG : MonoBehaviour
{
    public float opacityPerSecond = 1.0f;
    
    private SpriteRenderer _foreground = default!;
    private float _currentOpacity = 1.0f;
    private IEnumerator? _lastFade;
    
    void Start()
    {
        _foreground = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(_foreground);
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
        
        while (elapsedTime <= duration)
        {
            _currentOpacity = Mathf.Lerp(this._currentOpacity, targetOpacity, Math.Min(elapsedTime / duration, 1.0f));
            _foreground.color = new Color(1f, 1f, 1f, _currentOpacity);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private float CalculateDuration(float targetOpacity)
    {
        return Math.Abs(_currentOpacity - targetOpacity) / opacityPerSecond;
    }

    private void StopLastFade()
    {
        if (_lastFade != null)
            StopCoroutine(_lastFade);
    }
}
