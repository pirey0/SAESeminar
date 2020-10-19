using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidbody : MonoBehaviour
{
    [SerializeField] bool _useGravity = true;
    [SerializeField] float _mass = 1;

    [SerializeField] private Vector3 _velocity;
    [SerializeField] private Vector3 _angularVelocity;

    public float Mass { get => _mass; }
    public Vector3 Velocity { get => _velocity; set => _velocity = value; }

    private void FixedUpdate()
    {
        if (_useGravity)
        {
            _velocity += Physics.gravity * Time.fixedDeltaTime;
        }

        transform.position += _velocity * Time.fixedDeltaTime;
        transform.Rotate(_angularVelocity * Time.fixedDeltaTime);
    }


}
