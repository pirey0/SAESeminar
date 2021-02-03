using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    string name;
    List<RoomObject> containedObjects;

    public event System.Action<RoomObject> ObjectEntered;
    public event System.Action<RoomObject> ObjectLeft;

    public Room(string name)
    {
        this.name = name;
        containedObjects = new List<RoomObject>();
    }

    public void AddObject(RoomObject obj)
    {
        if (!containedObjects.Contains(obj))
        {
            containedObjects.Add(obj);
            ObjectEntered?.Invoke(obj);
        }
    }

    public void DestroyAll()
    {
        for (int i = 0; i < containedObjects.Count; i++)
        {
            GameObject.Destroy(containedObjects[i].gameObject);
        }

        containedObjects.Clear();
    }

    public void RemoveObject(RoomObject obj)
    {
        bool res = containedObjects.Remove(obj);
        if (res)
        {
            ObjectLeft?.Invoke(obj);
        }
    }

    public int GetCountOfType(RoomObjectType t)
    {
        int count = 0;

        for (int i = 0; i < containedObjects.Count; i++)
        {
            if (containedObjects[i].Type == t)
                count++;
        }
        return count;
    }
}
