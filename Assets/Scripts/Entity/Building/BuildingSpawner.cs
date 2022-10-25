using UnityEngine;
using Unity.Mathematics;

public class BuildingSpawner : SingletonMono<BuildingSpawner>
{
    public void Spawn(BaseBuilding building,Vector2 pos)
    {
        int2 index = GridManager.Instance.GetIndex(pos);
        Vector2 modifiedPos = GridManager.Instance.GetPos(index);
        Instantiate(building, new Vector3(modifiedPos.x, modifiedPos.y, 0),Quaternion.identity);
    }
}
