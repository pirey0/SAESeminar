using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VectorVisualizer : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float headLength;

    [Range(1, 90)]
    [SerializeField] float headAngle;

    [SerializeField] Arrow[] arrows;


    private void OnDrawGizmos()
    {
        
        if (arrows == null)
            return;

        foreach (var arrow in arrows)
        {
            if (arrow.Direction.magnitude > 0)
                DrawArrow(arrow, headLength, headAngle);
        }
    }

    public static void DrawArrow(Arrow arrow, float headLength, float headAngle)
    {
        DrawArrow(arrow.Position, arrow.Direction, arrow.Color, headLength, headAngle);
    }

    public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(180 + arrowHeadAngle, 0, 0) * new Vector3(0, 0, 1);
        Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(180 - arrowHeadAngle, 0, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, up * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, down * arrowHeadLength);

    }

}

[System.Serializable]
public class Arrow
{
    public Vector3 Position;
    public Vector3 Direction = Vector3.right;
    public Color Color = Color.white;
}
