using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FishInfo : MonoBehaviour
{
    public Sprite? placeholderSprite = default!; 
        
    public Image previewImage = default!;
    public TextMeshProUGUI fishNameText = default!;
    public TextMeshProUGUI fishWaterTypeText = default!;
    public TextMeshProUGUI fishDescriptionText = default!;
    
    [SerializeField] private BattleFishData? battleFishData;
    public bool autoRender = true;
    
    public BattleFishData? BattleFishData
    {
        get => battleFishData;
        set
        {
            battleFishData = value;
            if (autoRender)
                Render();
        }
    }
    
    private void Awake()
    {
        Assert.IsNotNull(previewImage, this + ": previewImage is missing.");
        Assert.IsNotNull(fishNameText, this + " : fishNameText is missing.");
        Assert.IsNotNull(fishWaterTypeText, this + " : fishWaterTypeText is missing.");
        Assert.IsNotNull(fishDescriptionText, this + " : fishDescriptionText is missing.");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            gameObject.SetActive(false);
    }

    public void Render()
    {
        if (battleFishData == null)
        {
            #pragma warning disable
            previewImage.sprite = placeholderSprite;
            #pragma warning restore
            fishNameText.text = "";
            fishWaterTypeText.text = "";
            fishDescriptionText.text = "";
        }
        else
        {
            previewImage.sprite = battleFishData.previewSprite;
            fishNameText.text = battleFishData.fishName;
            fishWaterTypeText.text = battleFishData.waterType.ToString();
            fishDescriptionText.text = battleFishData.description;
        }
        
        gameObject.SetActive(true);
    }
}