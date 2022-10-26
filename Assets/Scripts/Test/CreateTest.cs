using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CreateTest : MonoBehaviour
{
    public BuildingConfig towerConfig;
    public WaveConfig waveConfig;
    public BaseEnemy enemy;

    public Vector2 originPos;

    public Vector2 buildingPos;

    public Route way;

    private void Start()
    {
        GenBuilding();
        GenEnemies();
        //GenEnemy();
    }   

    public void GenBuilding()
    {
        BuildingManager.Instance.Spawn(towerConfig, buildingPos);
    }

    public void GenEnemies()
    {
        EnemyManager.Instance.Spawn(waveConfig, originPos);
    }

    public void GenEnemy()
    {
        EnemyManager.Instance.Spawn(enemy,originPos,way);
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
