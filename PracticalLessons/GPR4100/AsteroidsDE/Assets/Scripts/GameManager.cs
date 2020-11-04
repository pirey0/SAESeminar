using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] SpaceshipController player;

    private void Start()
    {
        instance = this;
    }

    public static void AddAsteroid(Asteroid a)
    {
        a.DestroyEvent += OnAsteroidDestroyed;
    }

    private static void OnAsteroidDestroyed()
    {
        instance.player.AddBullet();
    }
}
