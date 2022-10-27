using System.Collections.Generic;
using UnityEngine;

public class WaveCreator
{
    private List<EnemyCreator> enemyCreators;

    private Queue<TimeStageWaveSingle> waveQue;
    private TimeStageWaveSingle curWave;
    private float timeOffset;

    public WaveCreator()
    {
        waveQue = new Queue<TimeStageWaveSingle>();
        enemyCreators = new List<EnemyCreator>();
    }

    /// <summary>
    /// 重置波次数据
    /// </summary>
    public void Reset(TimeStageWaveConfig timeStageWaves, TimeStage timeStage)
    {
        waveQue.Clear();
        for (int i = 0; i < timeStageWaves.timeStageWaves.Length; i++)
            waveQue.Enqueue(timeStageWaves.timeStageWaves[i]);
        curWave = waveQue.Dequeue();
        timeOffset = (int) timeStage * GameDef.gameConfig.timeStageStepMinute * 60;
    }

    public void Update()
    {
        if (enemyCreators.Count > 0)
        {
            for (int i = 0; i < enemyCreators.Count; i++)
            {
                if (enemyCreators[i].Update())
                {
                    enemyCreators.RemoveAt(i--);
                }
            }
        }

        if (curWave != null)
        {
            float localTime = (DataManager.Instance.gameTime - timeOffset) / 60f;
            if (localTime >= curWave.createTime)
            {
                switch (curWave.waveType)
                {
                    case WaveType.Normal:
                        for (int i = 0; i < curWave.waves.Length; i++)
                            StartCreateEnemy(curWave.waves[i]);
                        break;
                    case WaveType.Pollute:
                        // 通过消耗污染值生成的怪物就一波就好了，调用多次木有意义
                        if (curWave.waves.Length > 0)
                            StartPolluteCreateEnemy(curWave.waves[0]);
                        break;
                    default: break;
                }

                if (waveQue.Count > 0)
                    curWave = waveQue.Dequeue();
                else curWave = null;
            }
        }
    }

    // 减少当前消耗当前污染值生成怪物
    private void StartPolluteCreateEnemy(WaveConfig waveConfig)
    {
        int count = Mathf.FloorToInt(DataManager.Instance.polluteTotal / GameDef.gameConfig.polluteToMonsterUnit);
        if (count <= 0) return;
        DataManager.Instance.polluteTotal -= count;
        EventManager.Dispath(GameEvent.UI_PollutionUpdate, DataManager.Instance.polluteTotal);
        Debug.Log($"通过消耗{count * GameDef.gameConfig.polluteToMonsterUnit}点污染值生成有{count}只的怪物波次");

        WaveConfig newWave = new WaveConfig();
        newWave.enemyType = waveConfig.enemyType;
        newWave.count = count;
        newWave.route = waveConfig.route;
        newWave.interval = waveConfig.interval;

        StartCreateEnemy(newWave);
    }

    // 开始生成怪物
    private void StartCreateEnemy(WaveConfig waveConfig)
    {
        EnemyCreator creator = new EnemyCreator(waveConfig);
        enemyCreators.Add(creator);
    }
}