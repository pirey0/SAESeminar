using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollisionHandler : MonoBehaviour
{
    MyCollider[] activeColliders;

    private void Start()
    {
        activeColliders = FindObjectsOfType<MyCollider>();
    }

    private void FixedUpdate()
    {
        //iterate through every possible collider pair and check (and then resolve) collision
        for (int i1 = 0; i1 < activeColliders.Length; i1++)
        {
            MyCollider c1 = activeColliders[i1];
            for (int i2 = i1 + 1; i2 < activeColliders.Length; i2++)
            {
                MyCollider c2 = activeColliders[i2];
                if (CheckForIntersectionBetween(c1, c2))
                {
                    Debug.Log(c1 + " is intersecting with " + c2);
                    ResolveCollisionFor(c1, c2);
                }
            }
        }

    }

    private void ResolveCollisionFor(MyCollider c1, MyCollider c2)
    {
        if (c1 is MySphereCollider && c2 is MySphereCollider)
        {
            ResolveCollisionForSphereres((MySphereCollider)c1, (MySphereCollider)c2);
        }
        else
        {
            throw new System.NotImplementedException("Collision resolution between collider types unknown");
        }
    }

    private void ResolveCollisionForSphereres(MySphereCollider c1, MySphereCollider c2)
    {
        MyRigidbody r1 = c1.Rigidbody;
        MyRigidbody r2 = c2.Rigidbody;

        if (r1 != null && r2 != null)
        {
            Vector3 difference = c2.Center - c1.Center;
            Vector3 normal = difference.normalized;

            Vector3 relativeVelocity = r2.Velocity - r1.Velocity;
            Vector3 normalVelocity = normal * Vector3.Dot(normal, relativeVelocity);

            r1.Velocity += normalVelocity; //adapt force
            r2.Velocity -= normalVelocity;

            c2.Center = c1.Center + normal * (c1.Radius + c2.Radius); //separate by force, push c2 away from c1
        }
        else
        {
            throw new System.NotImplementedException("Resolution without 2 Rigidbodies is not implemented");
        }
    }

    private bool CheckForIntersectionBetween(MyCollider c1, MyCollider c2)
    {
        if (c1 is MySphereCollider && c2 is MySphereCollider)
        {
            return CheckForIntersectionBetweenSpherers((MySphereCollider)c1, (MySphereCollider)c2);
        }

        throw new System.NotImplementedException("Intersection between collider types unknown");
    }

    private bool CheckForIntersectionBetweenSpherers(MySphereCollider s1, MySphereCollider s2)
    {
        float distance = (s2.Center - s1.Center).magnitude;
        float radiusSum = s1.Radius + s2.Radius;

        if (distance < radiusSum)
        {
            return true;
        }

        return false;
    }

}
