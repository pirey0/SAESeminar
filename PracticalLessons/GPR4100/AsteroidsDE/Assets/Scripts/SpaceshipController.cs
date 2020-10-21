using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : ScreenBoundObject
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _movementForce;
    [SerializeField] float _maxSpeed;

    [SerializeField] GameObject _bulletPrefab;

    Rigidbody2D _rigidbody;

    protected override void Start()
    {
        base.Start();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(0, 0, horizontal * (-1 * _rotationSpeed * Time.deltaTime));

        if (vertical > 0)
        {
            _rigidbody.AddForce(transform.up * _movementForce * vertical * Time.deltaTime);
        }

        if(_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }


        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(_bulletPrefab, transform.position + transform.up * 0.75f , transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        if(collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }
}
