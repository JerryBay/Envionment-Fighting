using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : SingletonMono<EnemyManager>
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

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
    
    public void Spawn(BaseEnemy enemy, Vector2 pos, Route route)
    {
        BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        inst.SetWayPoints(route.wayPoints);
    }
    
    public void Spawn(BaseEnemy enemy, Vector2 pos, List<Vector2> wayPoints)
    {
        BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        inst.SetWayPoints(wayPoints);
    }
}