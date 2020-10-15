using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDefinitionComponet : MonoBehaviour
{
    [SerializeField] string effectName;
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        EffectHandler.Instance.AddEffectDefinition(effectName, ApplyEffect);
    }

    private void ApplyEffect(EffectParams parameters)
    {
        Instantiate(prefab, parameters.Target.transform.position, Quaternion.identity);
    }
}
