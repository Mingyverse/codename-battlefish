using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class MapSelector : MonoBehaviour
{
    public StageData stageData = default!;
    public MapSelectorElements elements; 
    public ButtonColor buttonColors;

    private Image _image = default!;
    
    [Serializable]
    public struct MapSelectorElements
    {
        public TextMeshProUGUI mapNameText;
    }
    
    [Serializable]
    public struct ButtonColor
    {
        public Color unlocked;
        public Color locked;
    } 
    
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        
        Assert.IsNotNull(stageData);
        
        Render();
    }

    public void Render()
    {
        elements.mapNameText.text = stageData.stageName;
        
        if (stageData.IsDivingLocked() && stageData.IsBattleLocked()) 
            _image.color = buttonColors.locked;
        else
            _image.color = buttonColors.unlocked;
    }

    public void OnClick()
    {
        if (stageData.IsDivingLocked() && stageData.IsBattleLocked()) return;
        
        MapSelectController.instance.OpenMapInfo(this);
    }
    
    
}