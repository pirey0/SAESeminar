using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEffect
{
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void AddExampleEffect()
    {
        EffectHandler.Instance.AddEffectDefinition("Example", ApplyEffect);
    }

    public static void ApplyEffect(EffectParams effectParams)
    {
        Debug.Log("Example effect was applied to " + effectParams.Target.name);
    }
    
}
