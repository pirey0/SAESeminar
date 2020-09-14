using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float _t;
    [Range(0,3)]
    [SerializeField] float _speed = 1;

    #region Graph Drawing Parameters
    [Header("Graph Drawing")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] float _graphEndX;
    [SerializeField] int _graphPointCount;
    [SerializeField] bool _drawGraph;
    #endregion

    #region Setup
    private System.Func<float, float> _YFunction;

    private void Start()
    {
        _YFunction = LinearFunction;

        if (_drawGraph)
            DrawGraph();
    }

    private float LinearFunction(float x)
    {
        //F(x) = x
        return x;
    }
    #endregion

    void Update()
    {
        _t += Time.deltaTime * _speed;
        transform.position = new Vector3(_t, _t,0);
    }



    #region Graph Drawing
    private void DrawGraph()
    {
        if(_lineRenderer == null)
            return;

        Vector3[] positions = new Vector3[_graphPointCount];

        for (int i = 0; i < _graphPointCount; i++)
        {
            float x = _graphEndX * (((float)i) / _graphPointCount);
            positions[i] = new Vector3(x, _YFunction(x), 0);
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
