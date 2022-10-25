using UnityEngine;
using Unity.Mathematics;

public class BuildingSpawner : SingletonMono<BuildingSpawner>
{
    public void Spawn(BaseBuilding building,Vector2 pos)
    {
        Instantiate(building, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
    }
}
