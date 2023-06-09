using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BloodCell : MonoBehaviour
{
    private const float SplineOffset = 0.5f;
    [SerializeField] public SpriteShapeController spriteShape;
    [SerializeField] public Transform[] points;
    [SerializeField] private List<GameObject> borderPoints;

    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        Vector2 velocity = rigidbody2D.velocity;
        if (velocity.y > BloodCellsManager.Singleton.maxVelocity)
        {
            velocity.y = BloodCellsManager.Singleton.maxVelocity;
            rigidbody2D.velocity = velocity;
            foreach (GameObject borderPoint in borderPoints)
                borderPoint.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;
            Vector2 centerPosition = transform.position;

            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;

            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;

            try
            {
                spriteShape.spline.SetPosition(i, (vertex - towardsCenter * 1));
            }
            catch
            {
                Debug.Log("Spline points too close to each other");
                spriteShape.spline.SetPosition(i, vertex - towardsCenter * (colliderRadius + SplineOffset));
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 _newRt = Vector2.Perpendicular(towardsCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - (_newRt);

            spriteShape.spline.SetLeftTangent(i, _newLt);
            spriteShape.spline.SetRightTangent(i, _newRt);
        }
    }

}
