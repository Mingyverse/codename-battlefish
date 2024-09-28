using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFloat : MonoBehaviour
{
    public RectTransform buttonRectTransform;
    public float floatSpeed = 1.0f;
    public float floatHeight = 10.0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = buttonRectTransform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        buttonRectTransform.localPosition = new Vector3(originalPosition.x, originalPosition.y + newY, originalPosition.z);
    }
}
