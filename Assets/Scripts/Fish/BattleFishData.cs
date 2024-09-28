using System;
using UnityEngine;

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

    private static BattleFishData[] _cachedData = Array.Empty<BattleFishData>();
    
    public static BattleFishData[] GetAll(bool forceReload)
    {
        if (_cachedData.Length == 0 || forceReload)
            _cachedData = Resources.LoadAll<BattleFishData>("BattleFishData");
        
        return _cachedData;
    }

    public static BattleFishData[] GetData()
    {
        return GetAll(false);
    }
}