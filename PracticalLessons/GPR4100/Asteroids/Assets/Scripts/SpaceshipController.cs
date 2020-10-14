using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : ScreenBoundObject
{
    [SerializeField] float _thrusterForce;
    [SerializeField] float _rotationSpeed;

    Rigidbody2D rigidbody;

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

        transform.eulerAngles += new Vector3(0, 0, -1 * _rotationSpeed * horizontal * Time.deltaTime);
    }
    
}
