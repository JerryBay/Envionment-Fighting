using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DefenseTower : BaseBuilding
{
    public DefenseBuildingConfig defenseBuildingConfig;

    //Pollute
    public float polluteRate;
    
    //Attack
    public float attackDistance;
    public float attackInterval;
    public Area ammoArea;
    
    //Ammo
    public Ammo ammo;
    public float ammoDamage; 
    public float ammoSpeed;
    public AmmoType ammoType;
    public float ammoRange;

    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    public IntervalTimer intTimer;

    protected override void Awake()
    {
        base.Awake();
        Init();
        BuildingManager.Instance.towers.Add(this);
        DataManager.Instance.polluteRate += polluteRate;
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = attackDistance * 2;
    }

    private void Update()
    {
        intTimer.Update();
    }

    protected override void OnDestroy()
    {
        GridManager.Inst.BuildingRelease(this);
        if (BuildingManager.Instance.towers.Contains(this))
        {
            DataManager.Instance.polluteRate -= polluteRate;
            BuildingManager.Instance.towers.Remove(this);
        }

        base.OnDestroy();
    }

    private void Init()
    {
        polluteRate = defenseBuildingConfig.polluteRate;
        
        //Attack
        attackDistance = defenseBuildingConfig.attackDistance;
        attackInterval = defenseBuildingConfig.attackInterval;
        
        //Ammo
        ammo = defenseBuildingConfig.ammo;
        ammoArea = defenseBuildingConfig.ammoArea;
        ammoType = defenseBuildingConfig.ammoType;
        ammoDamage = defenseBuildingConfig.ammoDamage;
        ammoSpeed = defenseBuildingConfig.ammoSpeed;
        ammoRange = defenseBuildingConfig.ammoRange;


        intTimer = new IntervalTimer(attackInterval);
        intTimer.action = () =>
        {
            if (enemies.Count > 0)
            {
                Attack(enemies[0]);
            }
        };
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
        if ((enemy != null) && (enemy.area & ammoArea) != 0)
        {
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if ((enemy != null) && (enemy.area & ammoArea) != 0)
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    private void Attack(BaseEnemy enemy)
    {
        if (enemies[0] == null)
        {
            UpdateEnemies();
            return;
        }

        if (enemies.Count > 0)
        {
            Ammo bullet = Instantiate(ammo, transform);
            bullet.damage = ammoDamage;
            bullet.speed = ammoSpeed;
            bullet.type = ammoType;
            bullet.area = ammoArea;
            bullet.range = ammoRange;
            bullet.originTower = this;
            bullet.targetEnemy = enemy;
        }
    }
}