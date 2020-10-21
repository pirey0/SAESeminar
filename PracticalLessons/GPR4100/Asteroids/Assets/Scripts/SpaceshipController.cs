using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : ScreenBoundObject
{
    [SerializeField] float _thrusterForce;
    [SerializeField] float _rotationSpeed;

    [Range(1,10)]
    [SerializeField] float _maxSpeed;

    [SerializeField] Transform _bulletSpawn;
    [SerializeField] GameObject _bulletPrefab;

    [SerializeField] float _shootCooldown = 1;
    Rigidbody2D rigidbody;
    float _lastBulletTimestamp;


    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            rigidbody.AddForce(transform.up * _thrusterForce * Time.deltaTime);
        }

        //if higher then max speed, turn down
        float speed = rigidbody.velocity.magnitude;
        if (speed > _maxSpeed)
        {
            rigidbody.velocity = _maxSpeed * rigidbody.velocity.normalized;
        }

        transform.eulerAngles += new Vector3(0, 0, -1 * _rotationSpeed * horizontal * Time.deltaTime);


        bool shouldShoot = Input.GetButtonDown("Fire1");
        if (shouldShoot && CanShoot())
        {
            Instantiate(_bulletPrefab, _bulletSpawn.position, transform.rotation);
            _lastBulletTimestamp = Time.time;
        }

    }

    private bool CanShoot()
    {
        return Time.time - _lastBulletTimestamp > _shootCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Asteroid asteroid))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
    }
}
