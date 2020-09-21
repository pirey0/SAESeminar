using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float _t;
    [Range(0,3)]
    [SerializeField] float _speed = 1;
    [SerializeField] AnimationCurve curve1;

    #region Graph Drawing Parameters
    [Header("Graph Drawing")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] float _graphEndX;
    [SerializeField] int _graphPointCount;
    [SerializeField] bool _drawGraph;
    #endregion


    private void Start()
    {
        if (_drawGraph)
            DrawGraph();
    }

    void Update()
    {
        _t += Time.deltaTime * _speed;

        transform.position = new Vector3(XFunction(_t), YFunction(_t),0);
    }

    private float XFunction(float t)
    {
        return t;
    }

    private float YFunction(float t)
    {
        return curve1.Evaluate(t);
    }

    float Clamp(float value, float min, float max)
    {

        if (value < min)
            value = min;

        if (value > max)
            value = max;

        return value;
    }


    #region Graph Drawing
    private void DrawGraph()
    {
        if(_lineRenderer == null)
            return;

        Vector3[] positions = new Vector3[_graphPointCount];

        for (int i = 0; i < _graphPointCount; i++)
        {
            float t = _graphEndX * (((float)i) / _graphPointCount);
            positions[i] = new Vector3(XFunction(t), YFunction(t), 0);
        }

        _lineRenderer.positionCount = _graphPointCount;
        _lineRenderer.SetPositions(positions);
        Debug.Log("Drawing Graph");
    }

    private void ClearGraph()
    {
        if(_lineRenderer != null)
        _lineRenderer.positionCount = 0;
    }
    #endregion
}
