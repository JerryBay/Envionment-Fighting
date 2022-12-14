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
        var building = inst.GetComponent<BaseBuilding>();
        // if (building is DefenseTower)
        // {
        //     (building as DefenseTower).defenseBuildingConfig = config as DefenseBuildingConfig;
        // }
        // else if (building is ProductionBuilding)
        // {
        //     (building as ProductionBuilding).productionBuildingConfig = config as ProductionBuildingConfig;
        // }
        return building;
    }

    public void DestroyAll()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            Destroy(towers[i].gameObject);
        }

        for (int i = 0; i < prods.Count; i++)
        {
            Destroy(prods[i].gameObject);
        }
    }
}
