
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : SingletonMono<EnemySpawner>
{
    public void Spawn(BaseEnemy enemy, Vector2 pos, Route route)
    {
        //enemy.SetRoute(route);
        BaseEnemy inst = Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        inst.SetRoute(route);
    }
    
    public void Spawn(BaseEnemy enemy, Vector2 pos)
    {
        Instantiate(enemy, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
    }
}
