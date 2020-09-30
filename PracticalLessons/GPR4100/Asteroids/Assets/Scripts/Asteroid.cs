using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 movementVector;

    public float boundsRange;

    private void Update()
    {
        if (IsOutsideBounds())
        {
            transform.position = -transform.position;
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.position += movementVector * Time.deltaTime;

       
    }

    public bool IsOutsideBounds()
    {
        return transform.position.magnitude > boundsRange;
    }

}
