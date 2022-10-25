using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Route : MonoBehaviour
{
    public GameObject[] nodes;
    public List<Vector2> wayPoints;

    private void Awake()
    {
        wayPoints = new List<Vector2>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach (var transform in transforms)
        {
            Vector2 point = transform.position;
            wayPoints.Add(point);
        }
    }

    // public void GetWay(GameObject way)
    // {
    //     wayPoints = new List<Vector2>();
    //     Transform[] transforms = way.GetComponentsInChildren<Transform>();
    //     foreach (var transform in transforms)
    //     {
    //         Vector2 point = transform.position;
    //         wayPoints.Add(point);
    //     }
    // }
    //
    // public void GetWay(List<Vector2> way)
    // {
    //     wayPoints = way;
    // }
}
