using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBCollider : ACollider
{

    [SerializeField] Vector3 _size;
    public Vector3 Size { get => GetActualSize(); }

    private Vector3 GetActualSize()
    {
        return new Vector3(_size.x * transform.lossyScale.x, _size.y * transform.lossyScale.y, _size.z * transform.lossyScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
