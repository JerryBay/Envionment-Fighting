using System.Collections.Generic;

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
                for (int i = 0; i < curWave.waves.Length; i++)
                {
                    StartCreateEnemy(curWave.waves[i]);
                }

                if (waveQue.Count > 0)
                    curWave = waveQue.Dequeue();
                else curWave = null;
            }
        }
    }

    // 开始生成怪物
    private void StartCreateEnemy(WaveConfig waveConfig)
    {
        EnemyCreator creator = new EnemyCreator(waveConfig);
        enemyCreators.Add(creator);
    }
}