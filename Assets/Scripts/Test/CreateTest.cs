using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateTest : MonoBehaviour
{
    public DefenseBuildingConfig towerConfig;
    public ProductionBuildingConfig prodConfig;
    public WaveConfig waveConfig;
    public BaseEnemy enemy;

    public Vector2 originPos;

    [FormerlySerializedAs("buildingPos")] public Vector2 towerPos;
    public Vector2 prodPos;

    public Route way;

    private void Start()
    {
        GenTower();
        GenProd();
        GenEnemies();
        //GenEnemy();
    }

    public void GenProd()
    {
        BuildingManager.Instance.Spawn(prodConfig, prodPos);
    }

    public void GenTower()
    {
        BuildingManager.Instance.Spawn(towerConfig, towerPos);
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
