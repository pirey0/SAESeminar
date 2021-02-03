using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChecker : RoomObject
{
    [SerializeField] RoomsManager roomsManager;
    [SerializeField] RoomObjectType typeToWin;
    [SerializeField] int countToWin;

    private void Start()
    {

        CurrentRoom.ObjectEntered += OnObjectAdded;
        OnObjectAdded(null);
    }

    private void OnObjectAdded(RoomObject obj)
    {
        if (obj != null)
            Debug.Log("Object added: " + obj.name);

        CheckForVictory();
    }

    private void CheckForVictory()
    {
        if (CurrentRoom.GetCountOfType(typeToWin) >= countToWin)
        {
            Debug.Log("WIN!");
        }
    }
}
