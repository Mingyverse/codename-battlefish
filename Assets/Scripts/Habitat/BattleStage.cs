using System;
using System.Collections.Generic;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Habitat;

public class BattleStage : Habitat
{
    public new Dictionary<int, List<SpawnPos<BattleFish>>> FishPos = new Dictionary<int, List<SpawnPos<BattleFish>>>();
    public Stats BattleModifier;

    public void Start()
    {
        throw new NotImplementedException();
    }

    public int GetWinTeam()
    {
        throw new NotImplementedException();
    }

    public List<Item.Item> GetRewards()
    {
        throw new NotImplementedException();
    }
}