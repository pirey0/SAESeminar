using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _movementForce;

    Rigidbody2D _rigidbody;

    private float _halfHeight;
    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

        _halfHeight = _camera.orthographicSize;
        float halfWidth = _halfHeight * ((float)Screen.width / Screen.height);

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(0, 0, horizontal* ( - 1 * _rotationSpeed  * Time.deltaTime));

        if (vertical > 0)
        {
            _rigidbody.AddForce(transform.up * _movementForce * vertical * Time.deltaTime);
        }

        if(transform.position.y > _halfHeight)
        {
            transform.position = new Vector3(transform.position.x, - _halfHeight);
        }
    }

}
