using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : BaseBuilding
{
    public Area attackArea;
    public AmmoType attackType;

    public float damage;
    public float attackInterval;
    public float attackDistance;
    public Ammo ammo;
    
    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (enemies.Count > 0 && _timer >= attackInterval)
        {
            _timer = 0;
            Attack(enemies[0]);
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1111);
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy && enemy.area == attackArea)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy && enemy.area == attackArea)
        {
            enemies.Remove(enemy);
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
            Ammo bullet = Instantiate(ammo,transform);
            bullet.damage = damage;
            bullet.type = attackType;
            bullet.originTower = this;
            bullet.targetEnemy = enemy;
        }
    }
}
