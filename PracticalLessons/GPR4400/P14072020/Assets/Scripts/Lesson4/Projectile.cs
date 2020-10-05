using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector3 v0;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform collisionVisualization;

    [SerializeField] bool targetMode;
    [SerializeField] Transform target;

    Vector3? p0 = null;
    Vector3? hitPosition;

    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    
        p0 = transform.position;

        if (targetMode)
        {
            //F(v0) = (-gtt/2 -p0 +pt)/t where t = (pt-p0).magnitude

            Vector3 pt = target.position;
            float t = (pt - p0).Value.magnitude / 5;
            Vector3 g = Physics.gravity;

            v0 = (-g * t * t * 0.5f - p0.Value + pt)/t;
            Debug.Log(v0);
        }

        _rigidbody.velocity = v0;

        UpdateLineRenderer();
        CheckForCollision();

        if (hitPosition.HasValue)
        {
            collisionVisualization.position = hitPosition.Value;
            collisionVisualization.gameObject.SetActive(true);
        }
    }

    private void CheckForCollision()
    {
        float collisionDensityCheck = 0.25f;
        float collisionCheckAmount = 40;
        for (int i = 0; i < collisionCheckAmount; i++)
        {
            Vector3 pos0 = GetPositionAfterTime(i * collisionDensityCheck);
            Vector3 pos1 = GetPositionAfterTime((i + 1) * collisionDensityCheck);

            Vector3 v0to1 = pos1 - pos0;
            Ray r = new Ray(pos0, v0to1);
            if(Physics.Raycast(r, out RaycastHit hit, v0to1.magnitude))
            {
                hitPosition = hit.point;
                return;
            }
        }
    }

    private void UpdateLineRenderer()
    {
        Vector3[] positions = new Vector3[30];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = GetPositionAfterTime(i*0.1f);
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private Vector3 GetPositionAfterTime(float t)
    {
        //1/2 *gt^2 + v0t + p0;
        Vector3 result = 0.5f * Physics.gravity * t * t + v0 * t + p0.Value;

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + v0);

        if (!p0.HasValue)
        {
            p0 = transform.position;
        }

        for (float i = 0; i < 3; i+= 0.1f)
        {
            Vector3 positionAfterTime = GetPositionAfterTime(i);

            Gizmos.DrawSphere(positionAfterTime, 0.2f);
        }

    }
}
