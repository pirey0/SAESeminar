using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [SerializeField] float roomSeparationX;

    Room leftRoom, rightRoom;


    private void Awake()
    {
        leftRoom = new Room("Left Room");
        rightRoom = new Room("Right Room");

        var objecs = GameObject.FindObjectsOfType<RoomObject>();

        foreach (var obj in objecs)
        {
            var newRoom = GetRoomAt(obj.transform.position);
            obj.ChangeRoomTo(newRoom);
        }
    }

    public Room GetRoomAt(Vector3 position)
    {
        if (position.x < roomSeparationX)
        {
            return leftRoom;
        }
        else
        {
            return rightRoom;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(roomSeparationX, 0, 0), new Vector3(0, 10, 30));
    }
}
