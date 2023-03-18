using System;
using UnityEngine;

public class BloodCellsManager : MonoBehaviour
{
    public static BloodCellsManager Singleton;

    public float maxSideVelocity = 3.0f;
    public float maxVelocity = 5.0f;
    public float minVelocity = 1.0f;

    public float maxDistanceFromMouse = 5.0f;
    private void Start()
    {
        if (Singleton == null) Singleton = this;
        else return;
    }
}