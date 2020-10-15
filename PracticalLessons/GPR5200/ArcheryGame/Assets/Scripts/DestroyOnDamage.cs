using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDamage : MonoBehaviour, IEffectedDamagable
{
    public TakeDamageResult TakeDamage(float damageAmount)
    {
        Debug.Log(name + " took " + damageAmount + " damage.");
        Destroy(gameObject);
        return TakeDamageResult.Destroy;
    }

    public bool IgnoresEffect(string effectName)
    {
        return false;
    }
}
