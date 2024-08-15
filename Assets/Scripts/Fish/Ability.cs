using System;
using CodenameBattleFish.Context;

namespace CodenameBattleFish.Fish;

public class Ability
{
    public int ID;
    public string Name;

    private float _cooldownSeconds;
    private DateTime _lastProc;

    public bool CanActivate(CombatContext context)
    {
        throw new NotImplementedException();
    }

    public void Activate(CombatContext context)
    {
        throw new NotImplementedException();
    }

    public void AfterActivate()
    {
        throw new NotImplementedException();
    }
}