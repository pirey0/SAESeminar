using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASphereCollider : ACollider
{
    [SerializeField] float _radius;

    public float Radius { get => _radius; }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}


public class ACollider : MonoBehaviour
{
    [SerializeField] bool isTrigger;
}