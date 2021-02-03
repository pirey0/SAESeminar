using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMoveHandler : MonoBehaviour
{
    [SerializeField] RoomsManager roomsManager;

    MovableObject currentObject;
    Camera camera;

    Plane pickupPlane;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdatePickup();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TryRelease();
        }
    }

    private void UpdatePickup()
    {
        if (currentObject != null)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (pickupPlane.Raycast(ray, out float dist))
            {
                Vector3 newPos = ray.GetPoint(dist);
                currentObject.MoveTo(newPos);
            }
        }
    }

    private void TryRelease()
    {
        if (currentObject != null)
        {
            Room newRoom = roomsManager.GetRoomAt(currentObject.transform.position);
            currentObject.ChangeRoomTo(newRoom);
            currentObject.Release();
            currentObject = null;
        }
    }

    private void TryPickup()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 100);

        foreach (var h in hits)
        {
            if (h.transform.TryGetComponent(out MovableObject movableObject))
            {
                currentObject = movableObject;
                currentObject.Pickup();
                currentObject.RemoveFromRoom();

                pickupPlane = new Plane(Vector3.forward, h.point);

                break;
            }
        }
    }
}
