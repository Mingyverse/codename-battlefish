using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentFG : MonoBehaviour
{

    public SpriteRenderer? foreground;
    
    // Start is called before the first frame update
    void Start()
    {
        foreground = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOutSprite(float duration, float startOpacity, float targetOpacity)
    {
        float opacity;
        var time = 0f;
        while (time < duration)
        {
            opacity = Mathf.Lerp(startOpacity, targetOpacity, time / duration);
            foreground.color = new Color(1f, 1f, 1f, opacity);
            time += Time.deltaTime;
            yield return null;
        }

        opacity = targetOpacity;
        foreground.color = new Color(1f, 1f, 1f, opacity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOutSprite(0.7f, 1.0f, 0.7f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOutSprite(0.4f, 0.7f, 1.0f));
        }
    }
}
