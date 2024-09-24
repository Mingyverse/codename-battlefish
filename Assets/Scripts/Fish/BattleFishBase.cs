using UnityEngine;

[CreateAssetMenu(fileName = "NewBattleFish", menuName = "Codename BattleFish/BattleFish Data", order = 0)]
public class BattleFishBase : ScriptableObject
{
    public string fishName = default!;
    public string description = default!;
    public FishRarity rarity;
    public WaterType waterType;
    public FishStats.Stats baseStats = default!;
    public FishStats.Stats levelUpStats = default!;
    public FishClass fishClass;
    public XpScaling xpScaling;
    public FishAbility ability = default!;
    public FishAI ai = default!;
}