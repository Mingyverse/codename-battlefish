using System.Collections.Generic;
using CodenameBattleFish.AI;
using CodenameBattleFish.Habitat;

namespace CodenameBattleFish.Fish;

public class BattleFishType
{
    public int ID;
    public string Name = "";
    public string Description = "";
    public FishRarity Rarity;
    public WaterType WaterType;
    public FishGuide Guide;
    public FishAI AI;
    public Stats BaseStats;
    public Ability? Ability;
    public BattleClass BattleClass;
    public XpScaling XpScaling;
    public Dictionary<BattleFishType, int> NeighbourAffinity = new Dictionary<BattleFishType, int>();
}