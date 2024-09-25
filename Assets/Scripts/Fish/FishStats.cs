using System;

public class FishStats
{
    [Serializable]
    public class Stats
    {
        public float maxHp;
        public float attack;
        public float defense;
        public float attackSpeed;
        public float criticalRate;
        public float criticalMultiplier;
        public float range; 
        public float xpMultiplier;
        
        public static Stats operator +(Stats lhs, Stats rhs)
        {
            return new Stats
            {
                maxHp = lhs.maxHp + rhs.maxHp,
                attack = lhs.attack + rhs.attack,
                defense = lhs.defense + rhs.defense,
                attackSpeed = lhs.attack + rhs.attack,
                criticalRate = lhs.criticalRate + rhs.criticalRate,
                criticalMultiplier = lhs.criticalMultiplier + rhs.criticalMultiplier,
                range = lhs.range + rhs.range,
                xpMultiplier = lhs.xpMultiplier + rhs.xpMultiplier
            };
        }
        
        public static Stats operator *(Stats lhs, float rhs)
        {
            return new Stats
            {
                maxHp = lhs.maxHp * rhs,
                attack = lhs.attack * rhs,
                defense = lhs.defense * rhs,
                attackSpeed = lhs.attack * rhs,
                criticalRate = lhs.criticalRate * rhs,
                criticalMultiplier = lhs.criticalMultiplier * rhs,
                range = lhs.range * rhs,
                xpMultiplier = lhs.xpMultiplier * rhs
            };
        }
        
        public static Stats Zeros()
        {
            return new Stats
            {
                maxHp = 0,
                attack = 0,
                defense = 0,
                attackSpeed = 0,
                criticalRate = 0,
                criticalMultiplier = 0,
                range = 0,
                xpMultiplier = 0
            };
        }
        
        public static Stats Ones()
        {
            return new Stats
            {
                maxHp = 1.0f,
                attack = 1.0f,
                defense = 1.0f,
                attackSpeed = 1.0f,
                criticalRate = 1.0f,
                criticalMultiplier = 1.0f,
                range = 1.0f,
                xpMultiplier = 1.0f
            };
        }
    }
    
    public FishStats() {}

    public Stats baseStats = Stats.Zeros();
    public Stats constantStats = Stats.Zeros();
    public Stats multiplierStats = Stats.Ones();

    public float maxHp => (baseStats.maxHp + constantStats.maxHp) * multiplierStats.maxHp;
    public float attack => (baseStats.attack + constantStats.attack) * multiplierStats.attack;
    public float defense => (baseStats.defense + constantStats.defense) * multiplierStats.defense;
    public float attackSpeed => (baseStats.attackSpeed + constantStats.attackSpeed) * multiplierStats.attackSpeed;
    public float criticalRate => (baseStats.criticalRate + constantStats.criticalRate) * multiplierStats.criticalRate;
    public float criticalMultiplier => (baseStats.criticalMultiplier + constantStats.criticalMultiplier) * multiplierStats.criticalMultiplier;
    public float range => (baseStats.range + constantStats.range) * multiplierStats.range; 
    public float xpMultiplier => (baseStats.xpMultiplier + constantStats.xpMultiplier) * multiplierStats.xpMultiplier;

    public static FishStats FromFish(BattleFish fish)
    {
        Stats baseStats = fish.battleFishBase.baseStats;
        Stats leveledStats = fish.battleFishBase.levelUpStats * (fish.level.value - 1);
        return new FishStats
        {
            baseStats = baseStats + leveledStats,
        };
    }
}