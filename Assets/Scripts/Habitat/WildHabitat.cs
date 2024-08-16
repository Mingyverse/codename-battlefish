using System;
using System.Collections.Generic;
using System.Linq;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Habitat;

public class StageHabitat : Habitat
{
    public string StageNumber;
    public string StageName;
    public string StageDifficulty;
    public List<SpawnPos<BattleFish>> CollectedFish = new List<SpawnPos<BattleFish>>();
    public bool IsAlreadyCompleted = false;
    
    public float GetCompletionProgress()
    {
        return (float)CollectedFish.Count / FishPos.Count;
    }

    public List<SpawnPos<BattleFish>> GetUncollectedFish()
    {
        return FishPos.Except(CollectedFish).ToList();
    }

    public List<Item.Item> GetRewards()
    {
        throw new NotImplementedException();
    }

    public BattleFish CatchFish(SpawnPos<BattleFish> fish)
    {
        FishPos.Remove(fish);
        CollectedFish.Add(fish);
        throw new NotImplementedException();
    }
}