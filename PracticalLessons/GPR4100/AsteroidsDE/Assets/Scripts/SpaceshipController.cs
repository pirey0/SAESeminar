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
    [SerializeField] int startBullets = 10;

    Rigidbody2D _rigidbody;

    private int _currentBullets;

    public event System.Action BulletCountChanged;

    protected override void Start()
    {
        base.Start();
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentBullets = startBullets;
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

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_currentBullets > 0)
            {
                Instantiate(_bulletPrefab, transform.position + transform.up * 0.75f, transform.rotation);
                _currentBullets--;
                if(BulletCountChanged != null)
                    BulletCountChanged.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
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

    public int GetCurrentBulletCount()
    {
        return _currentBullets;
    }

    public  void AddBullet()
    {
        _currentBullets++;
        if (BulletCountChanged != null)
            BulletCountChanged.Invoke();
    }
}
