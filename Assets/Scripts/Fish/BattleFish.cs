using UnityEngine;

public class BattleFish : MonoBehaviour
{
    public BattleFishBase battleFishBase = default!;
    public FishStats stats;
    public Health health;
    public Level level;
    public FishAI ai = default!;
}