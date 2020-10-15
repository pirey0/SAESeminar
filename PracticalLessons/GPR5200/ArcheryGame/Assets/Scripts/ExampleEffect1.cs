using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect
{
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void AddExampleEffect()
    {
        EffectHandler.Instance.AddEffectDefinition("Fire", ApplyEffect);
    }

    public static void ApplyEffect(EffectParams effectParams)
    {
        effectParams.Damagable.TakeDamage(10);
        Debug.Log("Extra 10 fire damage");

    }
    
}
