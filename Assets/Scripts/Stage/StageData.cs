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

    private StageData? RecursivelyFindDivingPrerequisiteStage()
    {
        StageData? prerequisite = prerequisiteStage;
        while (prerequisite != null)
        {
            if (prerequisite.divingSceneName != "")
                return prerequisite;
                
            prerequisite = prerequisite.prerequisiteStage;
        }

        return null;
    }
    
    private StageData? RecursivelyFindBattlePrerequisiteStage()
    {
        StageData? prerequisite = prerequisiteStage;
        while (prerequisite != null)
        {
            if (prerequisite.battleSceneName != "")
                return prerequisite;
                
            prerequisite = prerequisite.prerequisiteStage;
        }

        return null;
    }
    
    public bool IsDivingLocked()
    {
        StageData? prerequisite = RecursivelyFindDivingPrerequisiteStage();
        if (prerequisite == null)
            return false;
        
        return !PlayerDataController.instance.completedDives.Contains(prerequisite.id);
    }
    
    public bool IsBattleLocked()
    {
        StageData? prerequisite = RecursivelyFindBattlePrerequisiteStage();
        if (prerequisite == null)
            return false;
        
        return !PlayerDataController.instance.completedBattles.Contains(prerequisite.id);
    }
}