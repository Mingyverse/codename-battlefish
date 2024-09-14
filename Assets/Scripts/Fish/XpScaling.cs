using System;

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
    /// <para>
    /// Uses the formula n/3 * x^3 + 5x^2,
    /// where n = growthFactor
    /// </para>
    /// <param name="xpScaling">The scaling constant</param>
    /// <param name="targetLevel">THe target level</param>
    /// <returns>The total required exp for <paramref name="targetLevel"/></returns>
    public static int GetRequiredXp(this XpScaling xpScaling, int targetLevel)
    {
        float growthFactor = (float)xpScaling / 100;

        return (int) Math.Ceiling(growthFactor / 3 * targetLevel * targetLevel * targetLevel + 5 * targetLevel * targetLevel);
    } 
}