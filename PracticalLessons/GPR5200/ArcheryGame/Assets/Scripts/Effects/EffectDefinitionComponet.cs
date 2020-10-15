using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectDefinitionComponet : MonoBehaviour
{
    [SerializeField] string effectName;

    protected virtual void Awake()
    {
        EffectHandler.Instance.AddEffectDefinition(effectName, ApplyEffect);
    }

    protected abstract void ApplyEffect(EffectParams parameters);
}
