using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    private void Update()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
        }else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1;
        }

        transform.Rotate(0, 0, -1 * rotationSpeed * horizontalInput * Time.deltaTime);
        transform.position += transform.up * moveSpeed * Time.deltaTime * verticalInput;
    }

}
