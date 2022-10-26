using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : BaseBuilding
{
    public ProductionBuildingConfig productionBuildingConfig;


    protected override void Awake()
    {
        base.Awake();
        BuildingManager.Instance.prods.Add(this);
    }

    protected override void OnDestroy()
    {
        GridManager.Inst.BuildingRelease(this);
        if (BuildingManager.Instance.prods.Contains(this))
        {
            BuildingManager.Instance.prods.Remove(this);
        }

        base.OnDestroy();
    }
}