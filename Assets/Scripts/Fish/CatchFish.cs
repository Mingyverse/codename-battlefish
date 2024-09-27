using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    
    private Animator parent;
    private GameObject fish;
    private bool isCatch;
    public static int fishCaught;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Animator>();
        fishCaught = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCatch = true;
            parent.SetBool("Catch", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isCatch = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            if (isCatch)
            {
                fish = other.gameObject;
                Destroy(fish);
                fishCaught++;
            }
        }
    }
}
