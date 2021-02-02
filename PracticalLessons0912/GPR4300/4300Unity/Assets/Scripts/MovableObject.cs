using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    private void Awake()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Pickup()
    {
        if (rigidbody != null)
            rigidbody.isKinematic = true;
    }

    public virtual void MoveTo(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public virtual void Release()
    {
        if (rigidbody != null)
            rigidbody.isKinematic = false;
    }
}
