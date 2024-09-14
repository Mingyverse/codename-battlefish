using System;
using UnityEngine;

public class BattleFish : MonoBehaviour
{
    public BattleFishBase battleFishBase = default!;
    public FishStats stats;
    
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level;

    private void Start()
    {
        health = GetComponentInChildren<Health>();
    }
}