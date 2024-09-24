using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public GameObject? progressMenu;
    
    // Start is called before the first frame update
    void Start()
    {

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
    }

    void ViewProgress()
    {
        progressMenu.SetActive(true);
    }

    void HideProgress()
    {
        progressMenu.SetActive(false);
    }
}
