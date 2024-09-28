using System;
using UnityEngine;

public class ForAllGameObjectsDo : MonoBehaviour
{
    public GameObject[] gameObjects = Array.Empty<GameObject>();
    
    public void InactiveAll()
    {
        foreach (GameObject go in gameObjects)
            go.SetActive(false);
    }
}