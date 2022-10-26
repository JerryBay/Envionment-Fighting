using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class BuildingManager : SingletonMono<BuildingManager>
{
    public List<DefenseTower> towers = new List<DefenseTower>();

    public List<ProductionBuilding> prods = new List<ProductionBuilding>();

    public BaseBuilding Spawn(BuildingConfig config, Vector2 pos)
    {
        var inst = Instantiate(config.prefab, new Vector3(pos.x, pos.y, 0),Quaternion.identity);
        return inst.GetComponent<BaseBuilding>();
    }
}
