using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameConfig", menuName = "创建游戏配置文件", order = 0)]
[Serializable]
public class GameConfig : ScriptableObject
{
    public BuildingConfigs[] buildings;
    public TimeStageWaveConfig[] timeStageWaves;
    public int timeStageStepMinute;
    public int polluteToMonsterUnit; // 污染值转换为怪物的数量单位

}