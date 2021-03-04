using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem slamParticles;
    [SerializeField] GameObject cameraObject;

    [SerializeField] UnityEngine.UI.Text nicknameText;
    [SerializeField] float moveSpeed, jumpVelocity, boostForce, slamForce;
    [SerializeField] Vector2 groundedCheckSize, groundedCheckOffset;
    [SerializeField] int jumpParticlesToEmit;

    private float horizontal;
    private float vertical;
    bool isBoosting;
    bool isSlamming;

    private void Start()
    {
        nicknameText.text = photonView.Owner.NickName;
        SetLocalColors();

        if (!photonView.IsMine)
        {
            cameraObject.SetActive(false);
        }
    }

    private void SetLocalColors()
    {
        Color color = GetColorForPlayerById(photonView.OwnerActorNr);
        spriteRenderer.color = color;
        nicknameText.color = color;

        var main = jumpParticles.main;
        main.startColor = color;

        trailRenderer.startColor = color;
        color.a = 0;
        trailRenderer.endColor = color;
    }

    public static Color GetColorForPlayerById(int id)
    {
        return Color.HSVToRGB((float)id / 10f, 1, 1);
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            var oldVertical = vertical;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            bool isGrounded = IsGrounded();

            if (isGrounded && vertical > 0)
            {
                Jump();
            }

            if (oldVertical <= 0 && vertical > 0)
            {
                StartBoosting();
            }
            else if (vertical <= 0 && oldVertical > 0)
            {
                StopBoosting();
            }

            if (vertical < 0 && oldVertical >= 0 && !isGrounded)
            {
                StartSlamming();
            }
            else if ((vertical >= 0 && oldVertical < 0) || isGrounded)
            {
                StopSlamming();
            }


            rigidbody.velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);

        }

        if (isBoosting)
        {
            rigidbody.AddForce(Vector2.up * boostForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        if (isSlamming)
        {
            rigidbody.AddForce(Vector2.down * slamForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }



    }

    private void StartBoosting()
    {
        photonView.RPC("RPC_StartBoosting", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_StartBoosting()
    {
        isBoosting = true;
        var emission = jumpParticles.emission;
        emission.rateOverTimeMultiplier = jumpParticlesToEmit;
    }

    private void StopBoosting()
    {

        photonView.RPC("RPC_StopBoosting", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_StopBoosting()
    {
        isBoosting = false;
        var emission = jumpParticles.emission;
        emission.rateOverTimeMultiplier = 0;
    }


    private void StartSlamming()
    {
        isSlamming = true;
        photonView.RPC("RPC_StartSlamming", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_StartSlamming()
    {
        slamParticles.Play();
    }

    private void StopSlamming()
    {
        isSlamming = false;
        photonView.RPC("RPC_StopSlamming", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_StopSlamming()
    {
        slamParticles.Stop();
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
    }

    private bool IsGrounded()
    {
        var hits = Physics2D.BoxCastAll(transform.position + (Vector3)groundedCheckOffset, groundedCheckSize, 0, Vector2.zero, 0);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3)groundedCheckOffset, groundedCheckSize);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(horizontal);
            stream.SendNext(vertical);
        }
        else
        {
            horizontal = (float)stream.ReceiveNext();
            vertical = (float)stream.ReceiveNext();
        }
    }
}
