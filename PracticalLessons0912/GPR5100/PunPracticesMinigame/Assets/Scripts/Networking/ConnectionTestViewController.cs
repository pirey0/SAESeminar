using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class ConnectionTestViewController : MonoBehaviour
{
    [SerializeField] ConnectionModel connectionModel;

    private string nickname = "NewPlayer";
    private string lastError;

    private void Start()
    {
        connectionModel.ConnectionError += OnError;
    }

    private void OnDestroy()
    {
        if (connectionModel != null)
            connectionModel.ConnectionError -= OnError;
    }

    private void OnError(string obj)
    {
        lastError = obj;
    }

    private void OnGUI()
    {
        GUI.color = Color.white;
        GUILayout.Label("State: " + PhotonNetwork.NetworkClientState);

        switch (PhotonNetwork.NetworkClientState)
        {
            case ClientState.PeerCreated:
                if (GUILayout.Button("Connect"))
                {
                    connectionModel.ConnectToServer();
                }
                break;

            case ClientState.ConnectedToMasterServer:
                if (GUILayout.Button("Join Lobby"))
                {
                    connectionModel.JoinDefaultLobby();
                }
                break;

            case ClientState.JoinedLobby:
                GUILayout.Label("Lobby: " + PhotonNetwork.CurrentLobby.Name);

                if (GUILayout.Button("Create Random"))
                {
                    connectionModel.CreateRandom();
                }
                if (GUILayout.Button("Join Random Random"))
                {
                    connectionModel.JoinRandomRoom();
                }

                foreach (var roomInfo in connectionModel.GetAllRooms())
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Room: " + roomInfo.Name + "(" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ")");

                    if (roomInfo.IsOpen)
                    {
                        if (GUILayout.Button("Join"))
                        {
                            connectionModel.JoinRoom(roomInfo.Name);
                        }
                    }

                    GUILayout.EndHorizontal();
                }

                break;

            case ClientState.Joined:
                var room = PhotonNetwork.CurrentRoom;

                GUILayout.Label(room.Name);
                GUILayout.Label("Players: " + room.PlayerCount);
                GUILayout.Label("-----");
                foreach (var pair in room.Players)
                {
                    GUILayout.Label(pair.Key + ": " + pair.Value.NickName + (pair.Value.IsMasterClient ? "(MasterClient)" : ""));
                }
                GUILayout.Label("-----");

                GUILayout.Label("LocalPlayer:");
                GUILayout.Label("Name:");
                nickname = GUILayout.TextField(nickname);
                connectionModel.RenameLocalPlayerTo(nickname);

                if (PhotonNetwork.IsMasterClient)
                {
                    if (GUILayout.Button("Play!"))
                    {
                        connectionModel.StartGame();
                    }
                }
                
                break;
        }

        GUI.color = Color.red;
        GUILayout.Label("LastError: " + lastError);
    }

}
