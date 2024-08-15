using System;
using CodenameBattleFish.Fish;

namespace CodenameBattleFish.AI;

using UnityEngine;

public abstract class FishAI : MonoBehaviour
{
    public int aggressiveness;
    public int speed;
    public BattleFish BattleFish;

    private DateTime _lastAttack;
    
    public abstract bool CanMove();
    public abstract void Move();
    public abstract bool CanAttack();
    public abstract void Attack();
}

