using System.Collections.Generic;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.Player;

public class FishAlmanac
{
    public HashSet<BattleFishType> DiscoveredFishes = new HashSet<BattleFishType>();

    public void DiscoverFish(BattleFishType fishType)
    {
        DiscoveredFishes.Add(fishType);
    }
}