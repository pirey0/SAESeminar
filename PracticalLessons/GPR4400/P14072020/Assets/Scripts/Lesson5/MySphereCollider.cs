using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySphereCollider : MyCollider
{

    public float Radius = 0.5f;



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Center, Radius);
    }

}
