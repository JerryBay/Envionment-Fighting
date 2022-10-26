using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : BaseBuilding
{
    public DefenseBuildingConfig defenseBuildingConfig;

    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    private void OnTriggerEnter(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy)
        {
            enemies.Remove(enemy);
        }
    }
    
    
}
