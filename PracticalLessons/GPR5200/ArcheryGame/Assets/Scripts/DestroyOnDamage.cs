using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDamage : MonoBehaviour, IDamagable
{
    public TakeDamageResult TakeDamage(float damageAmount)
    {
        Debug.Log(name + " took " + damageAmount + " damage.");
        Destroy(gameObject);
        return TakeDamageResult.Destroy;
    }
}
