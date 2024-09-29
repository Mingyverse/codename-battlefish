using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0f;
    }

    public void StageBegin()
    {
        Time.timeScale = 1f;
    }
}
