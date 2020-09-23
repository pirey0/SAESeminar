using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Pretty much everything one might ever need can actually be found here: https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs
public class MatrixHelper 
{

    public static Matrix4x4 GetTranslationMatrix(Vector3 position)
    {
        return new Matrix4x4(new Vector4(1, 0, 0, 0),
                             new Vector4(0, 1, 0, 0),
                             new Vector4(0, 0, 1, 0),
                             new Vector4(position.x, position.y, position.z, 1));
        
    }

    public static Matrix4x4 GetRotationMatrix(Vector3 eulerAngles, bool inRadians = false)
    {
        if(!inRadians)
            eulerAngles *= Mathf.Deg2Rad;

        Matrix4x4 rotationX = new Matrix4x4(new Vector4(1, 0, 0, 0),
                                            new Vector4(0, Mathf.Cos(eulerAngles.x), Mathf.Sin(eulerAngles.x), 0),
                                            new Vector4(0, -Mathf.Sin(eulerAngles.x), Mathf.Cos(eulerAngles.x), 0),
                                            new Vector4(0, 0, 0, 1));

        Matrix4x4 rotationY = new Matrix4x4(new Vector4(Mathf.Cos(eulerAngles.y), 0, -Mathf.Sin(eulerAngles.y), 0),
                                            new Vector4(0, 1, 0, 0),
                                            new Vector4(Mathf.Sin(eulerAngles.y), 0, Mathf.Cos(eulerAngles.y), 0),
                                            new Vector4(0, 0, 0, 1));

        Matrix4x4 rotationZ = new Matrix4x4(new Vector4(Mathf.Cos(eulerAngles.z), Mathf.Sin(eulerAngles.z), 0, 0),
                                            new Vector4(-Mathf.Sin(eulerAngles.z), Mathf.Cos(eulerAngles.z), 0, 0),
                                            new Vector4(0, 0, 1, 0),
                                            new Vector4(0, 0, 0, 1));

        return rotationX * rotationY * rotationZ;
    }

    public static Matrix4x4 GetScaleMatrix(Vector3 scale)
    {
        return new Matrix4x4(new Vector4(scale.x, 0, 0, 0),
                             new Vector4(0, scale.y, 0, 0),
                             new Vector4(0, 0, scale.z, 0),
                             new Vector4(0, 0, 0, 1));
    }

    public static Matrix4x4 TRS(Vector3 position, Vector3 rotationAngles, Vector3 scale)
    {
        return GetTranslationMatrix(position) * GetRotationMatrix(rotationAngles) * GetScaleMatrix(scale);
    }

    public static Quaternion GetRotation(Matrix4x4 m)
    {
        float w4 = Mathf.Sqrt(1 + m[0, 0] + m[1, 1] + m[2, 2]) * 2;
        float w = w4/4;

        float x = (m[2, 1] - m[1, 2]) / w4;
        float y = (m[0, 2] - m[2, 0]) / w4;
        float z = (m[1, 0] - m[0, 1]) / w4;

        return new Quaternion(x, y, z, w);
    }

    public static Vector3 GetPosition(Matrix4x4 m)
    {
        return new Vector3(m[0, 3], m[1, 3], m[2, 3]);
    }

    public static Vector3 GetScale(Matrix4x4 m)
    {
        var vx = m.GetColumn(0);
        var vy = m.GetColumn(1);
        var vz = m.GetColumn(2);
        vx.w = 0;
        vy.w = 0;
        vz.w = 0;

        return new Vector3(vx.magnitude, vy.magnitude, vz.magnitude);
    }

    public static void ApplyTRSTo(Transform t, Matrix4x4 matrix)
    {
        t.localPosition = matrix.MultiplyPoint3x4(Vector3.zero);
        t.localRotation = matrix.rotation;
        t.localScale = matrix.lossyScale;
    }
}
