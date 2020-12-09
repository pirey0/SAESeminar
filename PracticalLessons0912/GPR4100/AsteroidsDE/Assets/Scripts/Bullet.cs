using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ScreenBoundObject
{
    [SerializeField] private float _speed;

    protected override void Start()
    {
        base.Start();

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody != null)
        {
            rigidbody.velocity = transform.up * _speed;
        }
    }

}
