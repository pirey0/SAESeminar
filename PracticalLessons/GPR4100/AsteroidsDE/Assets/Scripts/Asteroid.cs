using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ScreenBoundObject
{
    public float MaxRotationSpeed;

    [SerializeField] private Vector3 velocity;

    private float randomRotationSpeed;

    protected override void Start()
    {
        base.Start();
        randomRotationSpeed = Random.Range(-1f, 1);
        velocity = Random.insideUnitCircle;
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(0, 0, randomRotationSpeed * MaxRotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

    }
}