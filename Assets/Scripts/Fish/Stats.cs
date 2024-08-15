namespace CodenameBattleFish.Fish;

public struct Stats
{
    public int MaxHp;
    public int Attack;
    public int Defense;
    public int AttackSpeed;
    public float CriticalRate;
    public float CriticalMultiplier;
    public float Range; 
    public float XpMultiplier;

    public Stats AddStats(Stats other)
    {
        return new Stats
        {
            MaxHp = MaxHp + other.MaxHp,
            Attack = Attack + other.Attack,
            Defense = Defense + other.Defense,
            AttackSpeed = AttackSpeed + other.AttackSpeed,
            CriticalRate = CriticalRate + other.CriticalRate,
            CriticalMultiplier = CriticalMultiplier + other.CriticalMultiplier,
            Range = Range + other.Range,
            XpMultiplier = XpMultiplier + other.XpMultiplier
        };
    }
}