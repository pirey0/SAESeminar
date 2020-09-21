using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CVector
{

    //magnitude
    //normalized
    //addition
    //multipication by scalar
    //dot product
    //cross product

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public float Magnitdue
    {
        get => (float)Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public CVector Normalized
    {
        get => Normalize(this);
    }

    public CVector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static CVector operator +(CVector v1, CVector v2)
    {
        return new CVector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static CVector operator *(CVector v, float s)
    {
        return new CVector(v.X * s, v.Y * s, v.Z * s);
    }

    public static CVector operator *(float s, CVector v)
    {
        return v * s;
    }

    public static CVector Normalize(CVector v)
    {
        var m = 1 / v.Magnitdue;
        return new CVector(v.X * m, v.Y * m, v.Z * m);
    }

    public static float Dot(CVector v1, CVector v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static CVector Cross(CVector v1, CVector v2)
    {
        float x = v1.Y * v2.Z - v2.Y * v1.Z;
        float y = v1.Z * v2.X - v2.Z * v1.X;
        float z = v1.X * v2.Y - v2.X * v1.Y;

        return new CVector(x, y, z);
    }

}
