using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSceneHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] string playerPrefabName;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = true;
        }
        else
        {
            CreatePlayerPrefab();
        }
    }

    private void CreatePlayerPrefab()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(playerPrefabName, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not connected to photon network");
        }
    }

    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.CreateRoom("OfflineMode123");
        }
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.OfflineMode)
        {
            CreatePlayerPrefab();
        }
    }
}
