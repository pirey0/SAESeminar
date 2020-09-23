using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ACollider[] colliders;


    private void FixedUpdate()
    {
        CheckForCollision();
    }

    private void CheckForCollision()
    {
        for (int a = 0; a < colliders.Length; a++)
        {
            for (int b = a + 1; b < colliders.Length; b++)
            {
                if (CollisionHelper.CheckOverlapBetween(colliders[a], colliders[b]))
                {
                    Debug.Log(colliders[a].name + "collides with " + colliders[b].name);
                }


            }
        }

    }

}
