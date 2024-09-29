using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    
    public static int fishCaught;
    private bool isCatch;
    public GameObject? indicator;
    
    // Start is called before the first frame update
    void Start()
    {
        fishCaught = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCatch = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isCatch = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BattleFish battleFish = other.GetComponentInParent<BattleFish>();
        if (battleFish == null) return;
        indicator.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isCatch)
        {
            BattleFish battleFish = other.GetComponentInParent<BattleFish>(); 
            if (battleFish == null) return;

            battleFish.gameObject.SetActive(false);
            fishCaught++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        indicator.SetActive(false);
    }
}