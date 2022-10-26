using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : BaseBuilding
{
    public DefenseBuildingConfig defenseBuildingConfig;

    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    private float _timer = 0;

    private void Awake()
    {
        BuildingManager.Instance.towers.Add(this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (enemies.Count > 0 && _timer >= defenseBuildingConfig.attackInterval)
        {
            _timer = 0;
            Attack(enemies[0]);
        }
    }

    private void OnDestroy()
    {
        if (BuildingManager.Instance.towers.Contains(this))
        {
            BuildingManager.Instance.towers.Remove(this);
        }
    }

    private void UpdateEnemies()
    {
        for (int i = enemies.Count; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy && enemy.area == defenseBuildingConfig.attackArea)
        {
            if (!enemies.Contains(enemy))
            {
                Debug.Log("enemy in");
                enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy && enemy.area == defenseBuildingConfig.attackArea)
        {
            if (enemies.Contains(enemy))
            {
                Debug.Log("enemy out");
                enemies.Remove(enemy);
            }
        }
    }

    private void Attack(BaseEnemy enemy)
    {
        if (enemies[0] == null)
        {
            UpdateEnemies();
        }

        if (enemies.Count > 0)
        {
            Ammo bullet = Instantiate(defenseBuildingConfig.ammo,transform);
            bullet.damage = defenseBuildingConfig.damage;
            bullet.speed = defenseBuildingConfig.ammoSpeed;
            bullet.type = defenseBuildingConfig.attackType;
            bullet.originTower = this;
            bullet.targetEnemy = enemy;
        }
    }
}
