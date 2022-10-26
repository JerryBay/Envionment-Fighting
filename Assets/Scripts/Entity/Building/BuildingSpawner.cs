using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class BuildingSpawner : SingletonMono<BuildingSpawner>
{
    public void SpawnBuildings(BuildingConfig config, Vector2 pos)
    {
        Instantiate(config.prefab, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
    }


}
