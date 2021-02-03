using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestroyer : MonoBehaviour
{
    [SerializeField] RoomsManager roomsManager;



    private void OnCollisionEnter(Collision collision)
    {
        var room = roomsManager.GetRoomAt(transform.position);
        room.DestroyAll();
    }
}
