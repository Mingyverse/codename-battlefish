using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FishInfo : MonoBehaviour
{
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


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.SetActive(false);        
    }
    
    public void Render()
    {
        if (battleFishData == null)
        {
            #pragma warning disable
            previewImage.sprite = null;
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