using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAABB : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;


    private void OnDrawGizmos()
    {
        if(mr != null)
        {
            Gizmos.DrawWireCube(mr.bounds.center, mr.bounds.size);
        }
    }
}
