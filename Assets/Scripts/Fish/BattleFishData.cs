using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "NewBattleFish", menuName = "Codename BattleFish/BattleFish Data", order = 0)]
public class BattleFishData : ScriptableObject
{
    public string id = "";
    public string fishName = "";
    public string description = "";
    public FishRarity rarity;
    public WaterType waterType;
    public FishStats.Stats baseStats = FishStats.Stats.Ones();
    public FishStats.Stats levelUpStats = FishStats.Stats.Ones();
    public FishClass fishClass;
    public XpScaling xpScaling;
    public Sprite previewSprite = default!;

    public static BattleFishData[] GetAll()
    {
        return Resources.LoadAll<BattleFishData>("BattleFishData");
    }
}