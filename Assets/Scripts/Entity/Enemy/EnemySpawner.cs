
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : SingletonMono<EnemySpawner>
{
    public void Spawn(BaseEnemy enemy, Vector2 pos, Route route)
    {
        //enemy.SetRoute(route);
        enemy.SetWayPoints(route.wayPoints);
        Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
    }
}
