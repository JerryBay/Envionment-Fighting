using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SingletonMono<EnemySpawner>
{
    public void SpawnEnemies(WaveConfig config, Vector2 pos)
    {
        StartCoroutine(Spawn(config,pos));
    }

    private IEnumerator Spawn(WaveConfig config,Vector2 pos)
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
    
    public void SpawnEnemy(BaseEnemy enemy, Vector2 pos, Route route)
    {
        BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        inst.SetWayPoints(route.wayPoints);
    }
    
    public void SpawnEnemy(BaseEnemy enemy, Vector2 pos, List<Vector2> wayPoints)
    {
        BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        inst.SetWayPoints(wayPoints);
    }
}