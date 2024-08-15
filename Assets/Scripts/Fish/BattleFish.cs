using System;
using System.Collections.Generic;
using CodenameBattleFish.Item;

namespace CodenameBattleFish.Fish;

public class BattleFish
{
    public BattleFishType BattleFishType { get; init; }
    public Stats BaseStats { get; set; }
    public Stats ModifierStats { get; set; }
    public int CurrentHp { get; private set; }
    public int Level { get; private set;  } = 1;
    public int Exp { get; private set; } = 0;
    public float Mood { get; private set; } = MaxMood;
    
    private static readonly float MaxMood = 120;

    public void ConsumeFood(Food food)
    {
        int affinity = food.FoodAffinity.GetValueOrDefault(BattleFishType, 1);
        Mood += 5 * affinity;
        Mood = Math.Min(Mood, MaxMood);
    }

    /// <summary>
    /// Increase the exp of the BattleFish from sources. Returns <value>true</value> when leveled up, else <value>false</value>.
    /// </summary>
    /// <param name="exp">The exp gained before modification</param>
    /// <returns><c>bool</c> to denote level up</returns>
    public bool IncreaseExp(int exp)
    {
        Exp += (int) (Exp * GetCurrentStats().XpMultiplier);

        if (Exp > BattleFishType.XpScaling.GetRequiredXp(Level + 1))
        {
            Level++;
            return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        CurrentHp = Math.Max(CurrentHp, 0);
    }

    public void RestoreHp(int restore)
    {
        CurrentHp += restore;
        CurrentHp = Math.Min(CurrentHp, BaseStats.MaxHp);
    }

    public Stats GetCurrentStats()
    {
        return BaseStats.AddStats(ModifierStats);
    }
}