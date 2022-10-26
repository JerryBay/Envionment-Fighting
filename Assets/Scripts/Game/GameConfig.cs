using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameConfig", menuName = "创建游戏配置文件", order = 0)]
[Serializable]
public class GameConfig : ScriptableObject
{
    public BuildingConfigs[] buildings;
    public WaveConfig[] monsterWaves;
}