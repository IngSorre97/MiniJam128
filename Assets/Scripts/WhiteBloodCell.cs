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
    }

    private void Update()
    {
        
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
        //Debug.Log(velocity);

    }


}
