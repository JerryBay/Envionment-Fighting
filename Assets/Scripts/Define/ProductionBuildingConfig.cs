using UnityEngine;

[CreateAssetMenu(fileName = "New ProductionBuildingConfig", menuName = "创建生产建筑")]
public class ProductionBuildingConfig : BuildingConfig
{
    public float productionRate; // 生产力
    public float polluteRate; // 污染力
}