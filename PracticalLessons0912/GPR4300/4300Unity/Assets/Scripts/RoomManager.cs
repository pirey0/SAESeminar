using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject _theWALL;


    private void Awake()
    {
        MovableObject[] allObjects;
        allObjects = GameObject.FindObjectsOfType<MovableObject>();

        Room firstRoom = new Room("Blue Room");
        Room secondRoom = new Room("Green Room");

        firstRoom.ObjectEntered += OnObjectEntered;

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].transform.position.x < _theWALL.transform.position.x)
            {
                firstRoom.AddObject(allObjects[i].gameObject);
            }
            else
            {
                secondRoom.AddObject(allObjects[i].gameObject);
            }
        }
        firstRoom.DestroyObjects();
        secondRoom.LogContent();

    }

    private void OnObjectEntered(GameObject obj)
    {
        Debug.Log(obj.name);
    }
}
