using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimeStageWaveConfig", menuName = "创建时间阶段波次")]
public class TimeStageWaveConfig : ScriptableObject
{
    public TimeStageWaveSingle[] timeStageWaves;
}

[Serializable]
public class TimeStageWaveSingle
{
    public float createTime; // 生成时长 (分钟)
    public WaveConfig[] waves; // 波次
}