using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundObject : MonoBehaviour
{
    private float _halfHeight;
    private float _halfWidth;

    protected virtual void Start()
    {
        Camera camera = Camera.main;
        _halfHeight = camera.orthographicSize;
        _halfWidth = _halfHeight * ((float)Screen.width / Screen.height);
    }

    protected virtual void Update()
    {
        if (transform.position.y > _halfHeight)
        {
            transform.position = new Vector3(transform.position.x, -_halfHeight);
        }
        else if (transform.position.y < -_halfHeight)
        {
            transform.position = new Vector3(transform.position.x, _halfHeight);
        }
        else if (transform.position.x > _halfWidth)
        {
            transform.position = new Vector3(-_halfWidth, transform.position.y);
        }
        else if (transform.position.x < -_halfWidth)
        {
            transform.position = new Vector3(_halfWidth, transform.position.y);
        }
    }

}
