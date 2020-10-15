using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float damage = 10;

    Rigidbody _rigidbody;
    private void Start()
    {
       _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.forward = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.collider.GetComponent<IDamagable>();
        if (damagable != null)
        {
            var result = damagable.TakeDamage(damage);

            if (result == TakeDamageResult.Destroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
