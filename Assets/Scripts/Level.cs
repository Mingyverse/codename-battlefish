namespace CodenameBattleFish;

public struct Level
{
    private BattleFish _fish;
    public int value { get; private set; } = 0;
    public int experience { get; private set; } = 0;
 
    public Level(BattleFish fish) => _fish = fish;
    
    /// <summary>
    /// Increase the exp of the BattleFish from sources. Returns <value>true</value> when leveled up, else <value>false</value>.
    /// </summary>
    /// <param name="exp">The exp gained before modification</param>
    /// <returns><c>bool</c> to denote level up</returns>
    public bool IncreaseExp(int exp)
    {
        experience += (int) (exp * _fish.stats.xpMultiplier);

        if (experience > _fish.battleFishBase.xpScaling.GetRequiredXp(value + 1))
        {
            value++;
            return true;
        }

        return false;
    }
}