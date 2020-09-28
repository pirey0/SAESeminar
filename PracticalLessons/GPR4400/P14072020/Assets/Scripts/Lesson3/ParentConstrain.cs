using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParentConstrain : MonoBehaviour
{

    [SerializeField] Transform parent, child;

    [SerializeField] Matrix4x4 parentMatrix;
    [SerializeField] Matrix4x4 childMatrix;

    [SerializeField] bool applyParent;

    private void Update()
    {
        if (parent == null || child == null)
            return;

        parent.localRotation = MatrixHelper.GetRotation(parentMatrix);
        parent.localScale = MatrixHelper.GetScale(parentMatrix);
        parent.localPosition = MatrixHelper.GetPosition(parentMatrix);


        Matrix4x4 childMatrixTransformed = childMatrix;

        if (applyParent)
            childMatrixTransformed = childMatrix * parentMatrix;


        child.localRotation = MatrixHelper.GetRotation(childMatrixTransformed);
        child.localScale = MatrixHelper.GetScale(childMatrixTransformed);
        child.localPosition = MatrixHelper.GetPosition(childMatrixTransformed);
    }

}
