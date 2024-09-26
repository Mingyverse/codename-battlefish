using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FishFilter : MonoBehaviour
{
    public Dropdown fishTypeDropdown;  
    public GameObject[] allFish;      
    public GameObject[] freshwaterFish;  
    public GameObject[] saltwaterFish;   

    void Start()
    {
        fishTypeDropdown.onValueChanged.AddListener(delegate {
            FilterFish(fishTypeDropdown.value);
        });
    }
    
    void FilterFish(int index)
    {
        SetFishActive(allFish, false);

        switch (index)
        {
            case 0:
                SetFishActive(allFish, true);
                break;
            case 1:
                SetFishActive(freshwaterFish, true);
                break;
            case 2:
                SetFishActive(saltwaterFish, true);
                break;
        }
    }
    
    void SetFishActive(GameObject[] fishArray, bool activeState)
    {
        foreach (GameObject fish in fishArray)
        {
            fish.SetActive(activeState);
        }
    }
}