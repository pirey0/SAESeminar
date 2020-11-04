using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ScreenBoundObject
{
    public float MaxRotationSpeed;

    [SerializeField] GameObject smallerAsteroid;
    [SerializeField] int amountToSpawn = 4;

    [SerializeField] private Vector3 velocity;

    private float randomRotationSpeed;

    public event System.Action DestroyEvent;

    protected override void Start()
    {
        base.Start();
        randomRotationSpeed = Random.Range(-1f, 1);
        velocity = Random.insideUnitCircle;
        GameManager.AddAsteroid(this);
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(0, 0, randomRotationSpeed * MaxRotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
            if (DestroyEvent != null)
                DestroyEvent.Invoke();

            if (smallerAsteroid != null)
            {
                for (int i = 0; i < amountToSpawn; i++)
                {
                    Instantiate(smallerAsteroid, transform.position, Quaternion.identity);
                }
            }
        }
    }
}