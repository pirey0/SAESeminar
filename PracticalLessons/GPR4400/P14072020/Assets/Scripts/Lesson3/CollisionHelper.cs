using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper 
{


    public static bool CheckOverlapBetween(ACollider a, ACollider b)
    {
        if (a is ASphereCollider && b is ASphereCollider)
        {

            var spc1 = (ASphereCollider)a;
            var spc2 = (ASphereCollider)b;

            return CheckOverlapBetween(spc1, spc2);
        }

        return false;
    }


    public static bool CheckOverlapBetween(ASphereCollider a, ASphereCollider b)
    {
        float dist = (b.transform.position - a.transform.position).magnitude;
        if (dist < a.Radius + b.Radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
