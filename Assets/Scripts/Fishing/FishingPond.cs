using System;
using System.Collections.Generic;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Fishing;

public class FishingPond
{
    public Dictionary<BattleFish, float> FishRate = new Dictionary<BattleFish, float>();
    public List<FishingProgress> ActiveFishings = new List<FishingProgress>();

    public FishingProgress Fish()
    {
        throw new NotImplementedException();
    }
}