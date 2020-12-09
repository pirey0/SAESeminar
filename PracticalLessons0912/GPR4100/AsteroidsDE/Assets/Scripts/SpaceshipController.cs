using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpaceshipController : ScreenBoundObject
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _movementForce;
    [SerializeField] float _maxSpeed;

    [SerializeField] GameObject _bulletPrefab, destructionEffectPrefab;
    [SerializeField] int startBullets = 10;
    [SerializeField] AudioSource _fireAudioSource, _thrustAudioSource;
    [SerializeField] float _thrustSoundTransitionSpeed;
    [SerializeField] ParticleSystem _engineParticleSystem;
    [SerializeField] float _emissionAmount = 100;

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


        ParticleSystem.EmissionModule emission = _engineParticleSystem.emission;
        if (vertical > 0)
        {
            _rigidbody.AddForce(transform.up * _movementForce * vertical * Time.deltaTime);

            _thrustAudioSource.volume += Time.deltaTime *_thrustSoundTransitionSpeed ;
            emission.rateOverTimeMultiplier = _emissionAmount;
        }
        else
        {
            _thrustAudioSource.volume -= Time.deltaTime * _thrustSoundTransitionSpeed;
            emission.rateOverTimeMultiplier = 0;
        }

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_currentBullets > 0)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        Instantiate(_bulletPrefab, transform.position + transform.up * 0.4f, transform.rotation);
        _currentBullets--;
        if (BulletCountChanged != null)
            BulletCountChanged.Invoke();

        if (_fireAudioSource != null)
            _fireAudioSource.Play();
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
        Instantiate(destructionEffectPrefab, transform.position, transform.rotation);
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }

    public int GetCurrentBulletCount()
    {
        return _currentBullets;
    }

    public void AddBullet()
    {
        _currentBullets++;
        if (BulletCountChanged != null)
            BulletCountChanged.Invoke();
    }
}
