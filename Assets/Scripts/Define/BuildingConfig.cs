using System;
using UnityEngine;

[Serializable]
public class BuildingConfigs
{
    public BuildingType buildingType;
    public BuildingConfig[] buildings;
}

[Serializable]
public class BuildingConfig : ScriptableObject
{
    public BuildingType type; // 类型
    public string name; // 名称
    public string desc; // 描述
    public Sprite icon; // 图标
    public GameObject prefab; // 预制体

    public float cost; // 所需货币
    public int level; // 级别
    public int rarity; // 稀有度

    public BuildingConfig nextLevel; // 下一级建筑，如果为null则表示不能继续升级
}