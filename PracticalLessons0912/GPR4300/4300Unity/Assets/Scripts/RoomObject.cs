using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    [SerializeField] RoomObjectType type;

    private Room currentRoom;
    public Room CurrentRoom { get => currentRoom; }
    public RoomObjectType Type { get => type; }

    public void ChangeRoomTo(Room newRoom)
    {
        if (currentRoom != null)
            currentRoom.RemoveObject(this);

        currentRoom = newRoom;

        if (newRoom != null)
            newRoom.AddObject(this);
    }

    public void RemoveFromRoom()
    {
        ChangeRoomTo(null);
    }
}

public enum RoomObjectType
{
    None,
    Sphere,
    Cylinder
}