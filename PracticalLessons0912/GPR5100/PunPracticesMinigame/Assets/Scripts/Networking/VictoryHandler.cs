using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VictoryHandler : MonoBehaviourPun
{
    [SerializeField] Text text;
    [SerializeField] Button resetButton;

    bool isFinished;

    private void Start()
    {
        resetButton.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient && !isFinished)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController pc))
            {
                int nr = pc.photonView.OwnerActorNr;
                Debug.Log("Trigger entered by " + nr);

                photonView.RPC("RPC_OnWin", RpcTarget.All, nr);
            }
        }

    }

    [PunRPC]
    private void RPC_OnWin(int winnerNr)
    {
        Debug.Log("RPC OnWin triggered with winnerdID " + winnerNr);
        Player p = PhotonNetwork.CurrentRoom.Players[winnerNr];

        text.gameObject.SetActive(true);
        text.color = PlayerController.GetColorForPlayerById(winnerNr);
        text.text = p.NickName + " won the Game!";
        isFinished = true;

        if (PhotonNetwork.IsMasterClient)
        {
            resetButton.gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_Reset", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_Reset()
    {
        Debug.Log("Resetting");
        PlayerController myPlayer = GameObject.FindObjectsOfType<PlayerController>().First((x) => x.photonView.IsMine);

        isFinished = false;
        myPlayer.transform.position = Vector3.zero;
        resetButton.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
