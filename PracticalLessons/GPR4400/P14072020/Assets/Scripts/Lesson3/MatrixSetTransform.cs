using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MatrixSetTransform : MonoBehaviour
{
    [SerializeField] Vector3 translation, euler, scale;
    void Update()
    {
        Matrix4x4 matrix = MatrixHelper.TRS(translation, euler, scale);
        transform.localRotation = MatrixHelper.GetRotation(matrix);
        transform.localScale = MatrixHelper.GetScale(matrix);
        transform.localPosition = MatrixHelper.GetPosition(matrix);
    }
}
