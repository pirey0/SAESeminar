using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixMeshVisualizer : MonoBehaviour
{
    [SerializeField] Mesh mesh;

    [SerializeField] Matrix4x4 matrixGreen = Matrix4x4.identity, matrixRed = Matrix4x4.identity;


    private void OnDrawGizmos()
    {
        var gizmosRenderingMatrix = Gizmos.matrix;

        Gizmos.matrix = matrixGreen;
        Gizmos.color = Color.green;
        Gizmos.DrawMesh(mesh, transform.position);

        Gizmos.matrix = matrixRed;
        Gizmos.color = Color.red;
        Gizmos.DrawMesh(mesh, transform.position);

        Gizmos.matrix = gizmosRenderingMatrix;
    }
}
