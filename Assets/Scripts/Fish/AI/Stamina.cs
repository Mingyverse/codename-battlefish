using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100; 
    public float value = 0;
    
    public float staminaRestorePerSecond = 20;
    public float staminaRestoreDelay = 2;

    public bool isRestoringStamina;
    public bool isFatigued;
    public float lastStaminaUse;

    private void Update()
    {
        if (CanRestoreStamina() && !isRestoringStamina)
            StartCoroutine(StartRestoreStamina());
    }

    public void RestoreStamina(float amount)
    {
        value += amount;
        value = Math.Min(value, maxStamina);

        if (value >= maxStamina - 0.0001f)
            isFatigued = false;
    }

    public void ConsumeStamina(float amount)
    {
        value -= amount;
        value = Math.Max(value, 0);
        
        lastStaminaUse = Time.time;
        if (value <= 0 + 0.0001f)
            isFatigued = true;
    }

    public bool CanRestoreStamina()
    {
        return value < maxStamina && lastStaminaUse + staminaRestoreDelay < Time.time;
    }
    
    public IEnumerator StartRestoreStamina()
    {
        isRestoringStamina = true;
        while (CanRestoreStamina())
        {
            RestoreStamina(staminaRestorePerSecond * Time.deltaTime);
            yield return null;
        }
        isRestoringStamina = false;
    }

}