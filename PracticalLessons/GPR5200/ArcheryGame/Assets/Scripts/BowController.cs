using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnTransform;
    [SerializeField] Transform previewArrow;
    [SerializeField] float arrowVelocityPerDrawSecond;
    [SerializeField] float minDrawTime;
    [SerializeField] float maxDrawTime;
    [SerializeField] float drawMovementPerSecond;


    float bowDrawTime = 0;

    private void Update()
    {
        BowUpdate();
        ArrowPreviewUpdate();
    }

    private void BowUpdate()
    {
        bool mouseDown = Input.GetMouseButton(0);

        if (mouseDown)
        {
            bowDrawTime += Time.deltaTime;
        }
        else
        {
            if (bowDrawTime > 0)
            {
                if(bowDrawTime > minDrawTime)
                {
                    ShootArrow();
                }
                bowDrawTime = 0;
            }
        }
    }

    private void ShootArrow()
    {
        var arrow = Instantiate(arrowPrefab, GetArrowPositionAtDrawTime(bowDrawTime), arrowSpawnTransform.rotation);
        var rigidbody = arrow.GetComponent<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.velocity = (Mathf.Min(maxDrawTime, bowDrawTime) * arrowVelocityPerDrawSecond * arrowSpawnTransform.forward);
        }
    }

    private void ArrowPreviewUpdate()
    {
        if (bowDrawTime <= 0)
        {
            previewArrow.gameObject.SetActive(false);
        }
        else
        {
            previewArrow.gameObject.SetActive(true);
            previewArrow.position = GetArrowPositionAtDrawTime(bowDrawTime);
        }
    }

    private Vector3 GetArrowPositionAtDrawTime(float t)
    {
        return arrowSpawnTransform.position + arrowSpawnTransform.forward * (maxDrawTime - Mathf.Min(maxDrawTime, t)) * drawMovementPerSecond;
    }
}
