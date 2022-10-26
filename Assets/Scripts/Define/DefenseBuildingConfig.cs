using UnityEngine;

[CreateAssetMenu(fileName = "New ProductionBuildingConfig", menuName = "创建防御建筑")]
public class DefenseBuildingConfig : BuildingConfig
{
    public Area attackArea; // 攻击范围
    public float damage; // 伤害
    public float damageRange;
    public float attackInterval; // 攻击间隔
    public float attackDistance; // 攻击距离
    
    public Ammo ammo; //弹药prefab
    public AmmoType attackType; //攻击类型
    public float ammoSpeed;
}