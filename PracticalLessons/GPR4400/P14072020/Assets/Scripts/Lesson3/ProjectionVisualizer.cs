using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionVisualizer : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] Mesh toRender;
    [SerializeField] Matrix4x4 matrix;

    private void OnDrawGizmos()
    {
        if (camera == null)
            return;

        matrix = camera.previousViewProjectionMatrix;
        Gizmos.matrix = matrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawMesh(toRender, new Vector3(0,0,5));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
