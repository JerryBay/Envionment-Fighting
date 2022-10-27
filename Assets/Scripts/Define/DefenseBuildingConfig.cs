using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New ProductionBuildingConfig", menuName = "创建防御建筑")]
public class DefenseBuildingConfig : BuildingConfig
{
    [Space]
    public float polluteRate;
    
    [Space]
    public float attackDistance; // 攻击距离
    public float attackInterval; // 攻击间隔
    
    [Space]
    public Ammo ammo; //弹药prefab
    public Area ammoArea; // 对空或者对地
    public AmmoType ammoType; //单体或者AOE
    public float ammoDamage; // 伤害
    public float ammoSpeed; //子弹速度
    public float ammoRange; //子弹攻击范围

}