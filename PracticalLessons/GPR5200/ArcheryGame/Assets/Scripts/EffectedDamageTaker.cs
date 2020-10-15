using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectedDamageTaker : MonoBehaviour, IEffectedDamagable
{
    public bool IgnoresEffect(string effectName)
    {
        return false;
    }

    public TakeDamageResult TakeDamage(float damageAmount)
    {
        Debug.Log(name + " took " + damageAmount + " damage");
        return TakeDamageResult.Destroy;
    }
}
