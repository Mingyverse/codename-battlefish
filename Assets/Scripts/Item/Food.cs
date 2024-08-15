using System.Collections.Generic;
using CodenameBattleFish.Fish;
using UnityEngine.Rendering;

namespace CodenameBattleFish.Item;

public class Food
{
    public Dictionary<BattleFishType, int> FoodAffinity = new SerializedDictionary<BattleFishType, int>();
}