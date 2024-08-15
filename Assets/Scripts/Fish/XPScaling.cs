using System;

namespace CodenameBattleFish.Fish;

/*
 * Divided with 100 to get growth factor
 */
public enum XpScaling
{
    Light = 10,
    Medium = 12,
    Heavy = 16
}

static class XpScalingMethods
{
    /// <summary>
    /// 
    /// </summary>
    /// <para>
    /// Uses the formula n/3 * x^3 + 5x^2,
    /// where n = growthFactor
    /// </para>
    /// <param name="xpScaling">The scaling constant</param>
    /// <param name="nextLevel"></param>
    /// <returns>The total required exp for <paramref name="nextLevel"/></returns>
    public static int GetRequiredXp(this XpScaling xpScaling, int nextLevel)
    {
        float growthFactor = (float)xpScaling / 100;

        return (int) Math.Ceiling(growthFactor / 3 * nextLevel * nextLevel * nextLevel + 5 * nextLevel * nextLevel);
    } 
}