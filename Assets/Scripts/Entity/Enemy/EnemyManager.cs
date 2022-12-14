using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : SingletonMono<EnemyManager>
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    public IntervalTimer oneSecond;

    private void Awake()
    {
        oneSecond = new IntervalTimer(1);
        oneSecond.action = Attack;
    }

    private void Update()
    {
        oneSecond.Update();
    }

    public void Attack()
    {
        float damage = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i]!=null)
            {
                if (enemies[i].startToAttack)
                {
                    damage += enemies[i].damage;
                }   
            }
        }
        
        DataManager.Instance.UpdatePopulation(-damage);
    }

    public void Spawn(WaveConfig config, Vector2 pos)
    {
        StartCoroutine(SpawnEnemy(config,pos));
    }

    private IEnumerator SpawnEnemy(WaveConfig config,Vector2 pos)
    {
        BaseEnemy enemy = config.enemyType;
        Route route = config.route;
        int count = config.count;
        float interval = config.interval;
        
        for (int i = 0; i < count; i++)
        {
            BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
            inst.SetWayPoints(route.wayPoints);
            yield return new WaitForSeconds(interval);
        }
    }

    public void DestroyAll()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();
    }
}