using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollider : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    public Vector3 Center { get => GetCenter(); set => SetCenter(value); }

    public MyRigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<MyRigidbody>();
    }

    private Vector3 GetCenter()
    {
        return transform.position + transform.rotation * _offset; 
    }

    private void SetCenter(Vector3 value)
    {
        transform.position = value - transform.rotation * _offset;
    }
}
