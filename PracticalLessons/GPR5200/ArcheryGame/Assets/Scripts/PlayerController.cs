using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float mouseXSpeed = 3, mouseYSpeed = 3;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnTransform;
    [SerializeField] Transform previewArrow;
    [SerializeField] float arrowVelocityPerDrawSecond;
    [SerializeField] float maxDrawTime;
    [SerializeField] float drawMovementPerSecond;

    CharacterController controller;
    Camera camera;
    float bowDrawTime = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Update()
    {
        MovementUpdate();
        BowUpdate();
        ArrowPreviewUpdate();
    }

    private void MovementUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        var input = horizontal * transform.right + vertical * transform.forward;
        controller.SimpleMove(input.normalized * speed);

        var angles = camera.transform.eulerAngles;

        float newXAngle = (angles.x > 180 ? angles.x - 360 : angles.x) - mouseY * mouseYSpeed;
        angles.x = Mathf.Clamp(newXAngle, -60, 60);
        angles.y += mouseX * mouseXSpeed;

        camera.transform.eulerAngles = angles;
    }

    private void BowUpdate()
    {
        bool mouseDown = Input.GetMouseButton(0);

        if (mouseDown)
        {
            bowDrawTime += Time.deltaTime;
        }
        else
        {
            if (bowDrawTime > 0)
            {
                var arrow = Instantiate(arrowPrefab, GetArrowPositionAtDrawTime(bowDrawTime), arrowSpawnTransform.rotation);
                var rigidbody = arrow.GetComponent<Rigidbody>();

                if (rigidbody)
                {
                    rigidbody.velocity = (Mathf.Min(maxDrawTime, bowDrawTime) * arrowVelocityPerDrawSecond * arrowSpawnTransform.forward);
                }
                bowDrawTime = 0;
            }
        }
    }

    private void ArrowPreviewUpdate()
    {
        if(bowDrawTime <= 0)
        {
            previewArrow.gameObject.SetActive(false);
        }
        else
        {
            previewArrow.gameObject.SetActive(true);
            previewArrow.position = GetArrowPositionAtDrawTime(bowDrawTime);
        }
    }

    private Vector3 GetArrowPositionAtDrawTime(float t)
    {
        return arrowSpawnTransform.position + arrowSpawnTransform.forward * (maxDrawTime - Mathf.Min(maxDrawTime, t)) * drawMovementPerSecond;
    }
}
