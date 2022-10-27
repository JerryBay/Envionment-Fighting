using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateTest : MonoBehaviour
{
    public DefenseBuildingConfig towerConfig;
    public Vector2 towerPos;
    public ProductionBuildingConfig prodConfig;
    public Vector2 prodPos;
    public WaveConfig waveConfig;
    public Vector2 originPos;

    private void Start()
    {
        GenTower();
        GenProd();
        GenEnemies();
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
}
