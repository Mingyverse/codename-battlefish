using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleFish", menuName = "Codename BattleFish/BattleFish Data", order = 0)]
public class BattleFishBase : ScriptableObject
{
    public string fishName = "";
    public string description = "";
    public FishRarity rarity;
    public WaterType waterType;
    public FishStats.Stats baseStats = FishStats.Stats.Ones();
    public FishStats.Stats levelUpStats = FishStats.Stats.Ones();
    public FishClass fishClass;
    public XpScaling xpScaling;
}