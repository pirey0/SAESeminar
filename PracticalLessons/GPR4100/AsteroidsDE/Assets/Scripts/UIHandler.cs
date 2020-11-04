using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Text bulletCountText;
    [SerializeField] SpaceshipController player;
    private void Start()
    {
        ChangeBulletCount();

        player.BulletCountChanged += ChangeBulletCount;
    }

    public void ChangeBulletCount()
    {
        bulletCountText.text = "Bullets: " + player.GetCurrentBulletCount();
    }
}
