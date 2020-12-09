using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectEffect : EffectDefinitionComponet
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float lifeTime;
    [SerializeField] bool oriented;

    protected override void ApplyEffect(EffectParams parameters)
    {
        var go = Instantiate(objectToSpawn, parameters.Target.transform.position, oriented? parameters.Target.transform.rotation : Quaternion.identity);
        if(lifeTime > 0)
        {
            Destroy(go, lifeTime);
        }
    }
}
