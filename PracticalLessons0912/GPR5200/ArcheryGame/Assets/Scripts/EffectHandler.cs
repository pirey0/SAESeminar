using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler
{
    private static EffectHandler instance;

    public delegate void EffectDelegate(EffectParams parameters);
    private Dictionary<string, EffectDelegate> effectsToMethod = new Dictionary<string, EffectDelegate>();

    public static EffectHandler Instance { get => GetInstance(); }

    private static EffectHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new EffectHandler();
        }
        return instance;
    }

    public void AddEffectDefinition(string name, EffectDelegate effect)
    {
        if (effectsToMethod.ContainsKey(name))
        {
            Debug.LogError("Attempting to define " + name + " twice, cancelled");
        }
        else
        {
            effectsToMethod.Add(name, effect);
        }
    }

    public void ApplyEffect(string effectName,EffectParams parameters)
    {
        if (effectsToMethod.ContainsKey(effectName))
        {
            effectsToMethod[effectName].Invoke(parameters);
        }
        else
        {
            Debug.LogError("Effect not defined: " + effectName);
        }
    }
}

public struct EffectParams
{
    public GameObject Target;
    public IEffectedDamagable Damagable;
    public object[] Parameters;

    public EffectParams(GameObject target, IEffectedDamagable damagable, params object[] paramenters)
    {
        Target = target;
        Damagable = damagable;
        Parameters = paramenters;
    }
}
