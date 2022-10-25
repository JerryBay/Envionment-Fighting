using System;
using UnityEngine;

[Serializable]
public class BuildingConfigs
{
    public BuildingType buildingType;
    public BuildingConfig[] buildings;
}

[Serializable]
public class BuildingConfig
{
    public string name; // 名称
    public string desc; // 描述
    public Sprite icon; // 图标
    public float money; // 所需货币
    public GameObject prefab; // 预制体
}