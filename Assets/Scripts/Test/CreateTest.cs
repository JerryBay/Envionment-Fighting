using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
    public BaseBuilding building;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(worldPos);
            int2 index =  GridManager.Instance.GetIndex(worldPos);
            Vector2 modifiedPos = GridManager.Instance.GetPos(index);
            //Debug.Log($"{worldPos} is {index}");
            BuildingSpawner.Instance.Spawn(building, modifiedPos);
        }
    }
}
