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
        TeleportWhenOutOfScreen();
    }

    private void TeleportWhenOutOfScreen()
    {
        if (transform.position.y > _halfHeight)
        {
            //above upper boundry
            transform.position = new Vector3(transform.position.x, -_halfHeight, transform.position.z);
        }
        else if (transform.position.y < -_halfHeight)
        {
            //below lower boundry
            transform.position = new Vector3(transform.position.x, _halfHeight, transform.position.z);
        }
        else if (transform.position.x > _halfWidth)
        {
            //to the right
            transform.position = new Vector3(-_halfWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -_halfWidth)
        {
            //to the left
            transform.position = new Vector3(_halfWidth, transform.position.y, transform.position.z);
        }
    }
}
