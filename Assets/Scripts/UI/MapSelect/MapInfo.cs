using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapInfo : MonoBehaviour
{
    [SerializeField] private StageData? stageData;
    public bool autoRender = true;
    
    public PreviewImage previewImage;
    public MapName mapName;
    public MapWaterType mapWaterType;
    public MapDescription mapDescription;
    public MapPrerequisite mapPrerequisite;
    public MapButtons mapButtons;

    public StageData? StageData
    {
        get => stageData;
        set
        {
            stageData = value;
            if (autoRender)
                Render();
        }
    }
    
    [Serializable]
    public struct PreviewImage
    {
        public Image image; 
            
        public Sprite? placeholderSprite;
    }
    
    [Serializable]
    public struct MapName
    {
        public TextMeshProUGUI text;
    }

    [Serializable]
    public struct MapWaterType
    {
        public TextMeshProUGUI text;
    }

    [Serializable]
    public struct MapDescription
    {
        public TextMeshProUGUI text;
    }

    [Serializable]
    public struct MapPrerequisite
    {
        public TextMeshProUGUI text;

        public Color unlockedColor;
        public Color lockedColor;
    }

    [Serializable]
    public struct MapButtons
    {
        public Button divingButton;
        public TextMeshProUGUI divingText;
        public Button battleButton;
        public TextMeshProUGUI battleText;
    }

    private void Awake()
    {
        Assert.IsNotNull(previewImage.image, this + ": Image is not set.");
        Assert.IsNotNull(mapName.text, this + ": MapName is not set.");
        Assert.IsNotNull(mapDescription.text, this + ": MapDescription is not set.");
        Assert.IsNotNull(mapWaterType.text, this + ": MapWaterType is not set.");
        Assert.IsNotNull(mapPrerequisite.text, this + ": MapPrerequisite is not set.");
        Assert.IsNotNull(mapButtons.divingButton, this + ": MapDivingButton is not set.");
        Assert.IsNotNull(mapButtons.divingText, this + ": MapDivingText is not set.");
        Assert.IsNotNull(mapButtons.battleButton, this + ": MapBattleButton is not set.");
        Assert.IsNotNull(mapButtons.battleText, this + ": MapBattleText is not set.");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            MapSelectController.instance.CloseMapInfo();
    }

    public void Render()
    {
        if (stageData == null) return;

        if (stageData.previewSprite != null)
            previewImage.image.sprite = stageData.previewSprite;
        else if (previewImage.placeholderSprite != null)
            previewImage.image.sprite = previewImage.placeholderSprite;
        
        mapName.text.text = stageData.stageName;
        mapWaterType.text.text = stageData.waterType.ToString();
        mapDescription.text.text = stageData.description;

        if (stageData.prerequisiteStage != null)
        {
            mapPrerequisite.text.text = "Cleared " + stageData.prerequisiteStage.stageName;
            mapPrerequisite.text.color = stageData.IsDivingLocked() ? mapPrerequisite.lockedColor : mapPrerequisite.unlockedColor;
        }
        else
        {
            mapPrerequisite.text.text = "None";
            mapPrerequisite.text.color = mapPrerequisite.unlockedColor;
        }
        
        bool isLocked = stageData.IsDivingLocked() || stageData.divingSceneName == "";
        mapButtons.divingButton.interactable = !isLocked;
        mapButtons.divingText.text = isLocked ? "Locked" : "Dive in";
        
        isLocked = stageData.IsBattleLocked() || stageData.battleSceneName == "";
        mapButtons.battleButton.interactable = !isLocked;
        mapButtons.battleText.text = isLocked ? "Locked" : "Battle";
        
        gameObject.SetActive(this);
    }
    
    public void LoadDivingScene()
    {
        if (stageData != null && !stageData.IsDivingLocked() && stageData.divingSceneName != "")
            SceneManager.LoadScene(stageData.divingSceneName);
    }

    public void LoadBattleScene()
    {
        if (stageData != null && !stageData.IsBattleLocked() && stageData.battleSceneName != "")
            SceneManager.LoadScene(stageData.battleSceneName);
    }

}