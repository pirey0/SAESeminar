using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IEffectedDamagable
{
    public bool IgnoresEffect(string effectName)
    {
        return false;
    }

    public TakeDamageResult TakeDamage(float damageAmount)
    {
        Debug.Log(name + " took " + damageAmount + " damage");
        return TakeDamageResult.Stuck;
    }
}
