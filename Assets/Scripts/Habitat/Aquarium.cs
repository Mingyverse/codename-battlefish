using System;
using System.Collections.Generic;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Habitat;

public class Aquarium : Habitat
{
    public new List<SpawnPos<BattleFish>> FishPos = new List<SpawnPos<BattleFish>>();
    public DateTime LastCleaned;
    
    public void ApplyItem(Item.Item item)
    {
        throw new NotImplementedException();
    }

    public void AddDecoration(Decoration deco)
    {
        throw new NotImplementedException();
    }

    public void CalculateExpGenerated(float deltaTime)
    {
        throw new NotImplementedException();
    }

    public void Clean()
    {
        throw new NotImplementedException();
    }
}