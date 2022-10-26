using UnityEngine;

[CreateAssetMenu(fileName = "New ProductionBuildingConfig", menuName = "创建防御建筑")]
public class DefenseBuildingConfig : BuildingConfig
{
    public Area attackArea; // 攻击范围
    public float damage; // 伤害
    public float attackSpeed; // 攻击速度
    public float attackDistance; // 攻击距离
}