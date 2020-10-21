using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ScreenBoundObject
{
    [SerializeField] private float _rotationMaxSpeed = 90;
    [SerializeField] private float _maximumMovmentSpeed = 1;
    [SerializeField] private Sprite[] _asteroidSprites;
    [SerializeField] GameObject _smallerAsteroidPrefab;
    [SerializeField] float _amountToSpawn;


    private float _randomRotationSpeed;
    private Vector3 _randomMovementDirection;

    protected override void Start()
    {
        base.Start();

        _randomRotationSpeed = Random.Range(-1f, 1f);
        _randomMovementDirection = Random.insideUnitCircle;

        if (_asteroidSprites != null && _asteroidSprites.Length > 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
        }
    }

    protected override void Update()
    {
        base.Update();
        transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * _randomRotationSpeed * _rotationMaxSpeed);
        transform.position += _randomMovementDirection * _maximumMovmentSpeed * Time.deltaTime;
    }

    public void Breakdown()
    {
        Destroy(gameObject);
        if (_smallerAsteroidPrefab != null)
        {
            for (int i = 0; i < _amountToSpawn; i++)
            {
                Instantiate(_smallerAsteroidPrefab, transform.position, Quaternion.identity);
            }
        }
    }

}
