using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
    public BaseBuilding building;
    public BaseEnemy enemy;

    public Vector2 originPos;

    public Vector2 buildingPos;

    public Route way;

    private void Start()
    {
        GenBuilding();
        GenEnemy();
    }   

    public void GenBuilding()
    {
        BuildingSpawner.Instance.Spawn(building, buildingPos);
    }

    public void GenEnemy()
    {
        EnemySpawner.Instance.Spawn(enemy,originPos,way);
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         //Debug.Log(worldPos);
    //         int2 index =  GridManager.Instance.GetIndex(worldPos);
    //         Vector2 modifiedPos = GridManager.Instance.GetPos(index);
    //         //Debug.Log($"{worldPos} is {index}");
    //         BuildingSpawner.Instance.Spawn(building, modifiedPos);
    //     }
    // }
}
