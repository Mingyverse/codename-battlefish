using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Codename BattleFish/Stage Data", order = 0)]
public class StageData : ScriptableObject
{
    public string id = "";
    public StageData? prerequisiteStage;
    public string stageName = "";
    public string description = "";
    public WaterType waterType;
    public Sprite previewSprite = default!;

    public string divingSceneName = "";
    public string battleSceneName = "";

    public bool IsDivingLocked()
    {
        if (prerequisiteStage == null)
            return false;
        
        return !PlayerDataController.instance.completedDives.Contains(prerequisiteStage.id);
    }
    
    public bool IsBattleLocked()
    {
        if (prerequisiteStage == null)
            return false;
        
        return !PlayerDataController.instance.completedBattles.Contains(prerequisiteStage.id);
    }
}