using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ScreenBoundObject
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    protected override void Start()
    {
        base.Start();

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody)
        {
            rigidbody.velocity = transform.up * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Asteroid asteroid))
        {
            Destroy(gameObject);
            asteroid.Breakdown();
        }
    }
}
