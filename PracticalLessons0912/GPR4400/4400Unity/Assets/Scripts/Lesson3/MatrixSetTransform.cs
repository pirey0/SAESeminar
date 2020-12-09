using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MatrixSetTransform : MonoBehaviour
{
    [SerializeField] Vector3 translation, euler, scale;
    [SerializeField] bool drawGrid;
    [SerializeField] int drawGridCount;
    [SerializeField] float drawGridStepSize;

    [SerializeField] bool setManually;
    [SerializeField] Matrix4x4 matrix;
    void Update()
    {
        if (!setManually)
        {
            matrix = MatrixHelper.TRS(translation, euler, scale);
        }

        transform.localRotation = MatrixHelper.GetRotation(matrix);
        transform.localScale = MatrixHelper.GetScale(matrix);
        transform.localPosition = MatrixHelper.GetPosition(matrix);
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Vector3 pos = MatrixHelper.GetPosition(matrix);
        Vector3 vx = MatrixHelper.GetXVector(matrix);
        Vector3 vy = MatrixHelper.GetYVector(matrix);
        Vector3 vz = MatrixHelper.GetZVector(matrix);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + vx);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, pos + vy);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + vz);


        if (drawGrid)
        {
            Gizmos.color = Color.white;
            int count = Mathf.Min(drawGridCount, 25);
            for (int x = 0; x < count; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    Vector3 p = MatrixHelper.MultiplyPoint(matrix, new Vector3(x * drawGridStepSize, y * drawGridStepSize, 0));

                    Gizmos.color = Color.blue;
                    Vector3 v = vz * drawGridCount * drawGridStepSize;
                    Gizmos.DrawLine(p, p + v);

                    p = MatrixHelper.MultiplyPoint(matrix, new Vector3(0, x * drawGridStepSize, y * drawGridStepSize));

                    Gizmos.color = Color.green;
                    v = vx * drawGridCount * drawGridStepSize;
                    Gizmos.DrawLine(p, p +v);

                    p = MatrixHelper.MultiplyPoint(matrix, new Vector3(x * drawGridStepSize,0, y * drawGridStepSize));

                    Gizmos.color = Color.red;
                    v = vy * drawGridCount * drawGridStepSize;
                    Gizmos.DrawLine(p, p + v);
                }
            }
        }

    }

}
