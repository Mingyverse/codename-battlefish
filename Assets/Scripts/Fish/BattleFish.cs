using System;
using UnityEngine;

public class BattleFish : MonoBehaviour
{
    public BattleFishBase battleFishBase = default!;
    [NonSerialized] public FishStats stats = default!;
    [NonSerialized] public Health health = default!;
    [NonSerialized] public Level level;

    private void Start()
    {
        health = GetComponentInChildren<Health>();
    }
}