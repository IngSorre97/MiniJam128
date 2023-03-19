using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShape;
    [SerializeField] private Transform[] points;
    private const float _splineOffset = 0.5f;

    void Start()
    {
        UpdateVertices();
    }

    void Update()
    {
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;
            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;
            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                spriteShape.spline.SetPosition(i, vertex - towardsCenter);
            }
            catch
            {
                Debug.Log("Spline points too close, recalculate");
                spriteShape.spline.SetPosition(i, vertex - towardsCenter * (colliderRadius + _splineOffset));
            }

            Vector2 lt = spriteShape.spline.GetLeftTangent(i);
            Vector2 newRt = Vector2.Perpendicular(towardsCenter) * lt.magnitude;
            Vector2 newLt = Vector2.zero - newRt;
            
            spriteShape.spline.SetRightTangent(i, newRt);
            spriteShape.spline.SetLeftTangent(i, newLt);
        }
    }
}
