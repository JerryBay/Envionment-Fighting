using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : BaseBuilding
{
    public ProductionBuildingConfig productionBuildingConfig;
    
    private void Awake()
    {
        BuildingManager.Instance.prods.Add(this);
    }
    
    private void OnDestroy()
    {
        GridManager.Inst.BuildingRelease(this);
        if (BuildingManager.Instance.prods.Contains(this))
        {
            BuildingManager.Instance.prods.Remove(this);
        }
    }
}
