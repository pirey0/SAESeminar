using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float mouseXSpeed = 3, mouseYSpeed = 3;

    CharacterController controller;
    Camera camera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        Vector3 camForward = camera.transform.forward;
        camForward.y = 0;
        Vector3 camRight = camera.transform.right;
        camRight.y = 0;
        var input = horizontal * camRight + vertical * camForward;
        controller.SimpleMove(input.normalized * speed);

        var angles = camera.transform.eulerAngles;

        float newXAngle = (angles.x > 180 ? angles.x - 360 : angles.x) - mouseY * mouseYSpeed;
        angles.x = Mathf.Clamp(newXAngle, -60, 60);
        angles.y += mouseX * mouseXSpeed;

        camera.transform.eulerAngles = angles;
    }

}
