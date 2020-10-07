using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float MaxRotationSpeed;

    [SerializeField] private Vector3 velocity;

    private float randomRotationSpeed;

    private void Start()
    {
        randomRotationSpeed = Random.Range(-1f, 1);
        velocity = Random.insideUnitCircle;
    }

    private void Update()
    {
        transform.Rotate(0, 0, randomRotationSpeed * MaxRotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

    }
}