public struct FishStats
{
    public struct Stats
    {
        public float maxHp;
        public float attack;
        public float defense;
        public float attackSpeed;
        public float criticalRate;
        public float criticalMultiplier;
        public float range; 
        public float xpMultiplier;

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
}