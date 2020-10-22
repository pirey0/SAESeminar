using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    TakeDamageResult TakeDamage(float damageAmount);
}

public interface IEffectedDamagable : IDamagable
{
    bool IgnoresEffect(string effectName);
}


public enum TakeDamageResult
{
    Normal, 
    Destroy,
    Stuck 
}