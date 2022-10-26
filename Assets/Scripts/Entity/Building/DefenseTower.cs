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

    public DefenseBuildingConfig defenseBuildingConfig;


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

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log(1111);
    //     BaseEnemy enemy = col.otherCollider.GetComponent<BaseEnemy>();
    //     if (enemy && enemy.area == attackArea)
    //     {
    //         enemies.Add(enemy);
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     BaseEnemy enemy = other.collider.GetComponent<BaseEnemy>();
    //     if (enemy && enemy.area == attackArea)
    //     {
    //         enemies.Remove(enemy);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if (enemy && enemy.area == attackArea)
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
        if (enemy && enemy.area == attackArea)
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
            Ammo bullet = Instantiate(ammo,transform);
            bullet.damage = damage;
            bullet.type = attackType;
            bullet.originTower = this;
            bullet.targetEnemy = enemy;
        }
    }
}
