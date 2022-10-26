using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class EntitySpawner : SingletonMono<EntitySpawner>
{
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
    
    public void SpawnBuilding(BaseBuilding building,Vector2 pos)
    {
        int2 index = GridManager.Instance.GetIndex(pos);
        Vector2 modifiedPos = GridManager.Instance.GetPos(index);
        Instantiate(building, new Vector3(modifiedPos.x, modifiedPos.y, 0),Quaternion.identity);
    }
}
