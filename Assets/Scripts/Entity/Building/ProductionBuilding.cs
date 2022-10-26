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
        DataManager.Instance.polluteRate += productionBuildingConfig.polluteRate;
        DataManager.Instance.productivity += productionBuildingConfig.productionRate;
    }

    protected override void OnDestroy()
    {
        GridManager.Inst.BuildingRelease(this);
        if (BuildingManager.Instance.prods.Contains(this))
        {
            DataManager.Instance.polluteRate -= productionBuildingConfig.polluteRate;
            DataManager.Instance.productivity -= productionBuildingConfig.productionRate;
            BuildingManager.Instance.prods.Remove(this);
        }

        base.OnDestroy();
    }
}