using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class ProgressBar : MonoBehaviour
{

    public GameObject progressMenu = default!;
    public TextMeshProUGUI text = default!;
    public GameObject progressBar = default!;
    public GameObject pauseMenu = default!;
    public GameObject stageClear = default!;


    private void Awake()
    {
        Assert.IsNotNull(progressMenu);
        Assert.IsNotNull(text);
        Assert.IsNotNull(progressBar);
        Assert.IsNotNull(pauseMenu);
        Assert.IsNotNull(stageClear);
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && !PauseMenu.isPaused)
            ViewProgress();
        else
            HideProgress();
        
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

        if (progressPC > 99.9f)
        {
            stageClear.SetActive(true);
            pauseMenu.SetActive(false);
            PlayerDataController.instance.completedDives.Add(StageController.instance.stageData.id);
            Time.timeScale = 0f;
        }
        
    }
}
