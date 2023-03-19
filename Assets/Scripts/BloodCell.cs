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
    }

    private void Update()
    {
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
}

