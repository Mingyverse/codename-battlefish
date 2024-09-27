using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public GameObject? progressMenu;
    public TextMeshProUGUI text;
    public GameObject? progressBar;
    public GameObject? pauseMenu;
    public GameObject? stageClear;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && !PauseMenu.isPaused)
        {
            ViewProgress();
        }
        else
        {
            HideProgress();
        }
        
        UpdateProgressBar();
    }

    void ViewProgress()
    {
        progressMenu.SetActive(true);
    }

    void HideProgress()
    {
        progressMenu.SetActive(false);
    }

    void UpdateProgressBar()
    {
        float progressPC;
        progressPC = 100f * CatchFish.fishCaught / StageController.instance.wildFishes.Length;

        text.text = "Complete: " + (int)Math.Round(progressPC) + '%';

        progressBar.transform.localScale = progressBar.transform.localScale with { x = progressPC / 100 };

        if (progressPC == 100)
        {
            stageClear.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 0f;
        }
        
    }
}
