using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WhiteBloodCell : MonoBehaviour
{
    private const float SplineOffset = 0.5f;
    [SerializeField] private SpriteShapeController spriteShape;
    [SerializeField] private Transform[] points;
    [SerializeField] private List<GameObject> borderPoints;

    private Vector2 _mousePosition;

    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
        
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        Vector2 distance = _mousePosition - (Vector2) transform.position;

        Vector2 velocity;
        float xDistance = distance.x > 0 
            ? Mathf.Max(distance.x, BloodCellsManager.Singleton.maxDistanceFromMouse) 
            : Mathf.Min(distance.x, - BloodCellsManager.Singleton.maxDistanceFromMouse);
        
        velocity.x = (xDistance / BloodCellsManager.Singleton.maxDistanceFromMouse) * BloodCellsManager.Singleton.maxSideVelocity;
        float yDistance = Mathf.Max(Mathf.Max(0, distance.y), BloodCellsManager.Singleton.maxDistanceFromMouse);
        velocity.y = (yDistance / BloodCellsManager.Singleton.maxDistanceFromMouse) * BloodCellsManager.Singleton.maxVelocity;
        rigidbody2D.velocity = velocity;
        Debug.Log(velocity);

    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < points.Length-1; i++)
        {
            Vector2 vertex = points[i].localPosition;
            Vector2 towardsCenter = Vector2.zero - vertex;
            towardsCenter.Normalize();

            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            spriteShape.spline.SetPosition(i,  towardsCenter * (colliderRadius * 2.0f));

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 _newRt = Vector2.Perpendicular(towardsCenter) * _lt.magnitude;
            Vector2 _newLt = Vector2.zero - (_newRt);

            spriteShape.spline.SetLeftTangent(i, _newLt);
            spriteShape.spline.SetRightTangent(i, _newRt);
        }
    }

}
