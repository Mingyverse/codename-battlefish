using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoseMenu : MonoBehaviour
{

    public GameObject? winUI;
    public GameObject? loseUI;
    public BattleFish[] ownFish;
    public BattleFish[] enemyFish;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        checkFishDead();
    }

    void checkFishDead()
    {
        for (int i = 0; i < ownFish.Length; i++)
        {
            if (AllFalse(ownFish))
            {
                StartCoroutine(Wait(loseUI));
            }
            else if (AllFalse(enemyFish))
            {
                StartCoroutine(Wait(winUI));
            }
        }
    }

    IEnumerator Wait(GameObject scene)
    {
        yield return new WaitForSeconds(1.5f);
        scene.SetActive(true);
        Time.timeScale = 0f;
    }

    private bool AllFalse(BattleFish[] fishes)
    {
        for (int i = 0; i < fishes.Length; i++)
        {
            if (!fishes[i].health.isDead) return false;
        }
        return true;
    }
}
