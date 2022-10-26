using UnityEngine;

public class EnemyCreator
{
    private float curTime;
    private WaveConfig wave;
    private int createCount;

    public EnemyCreator(WaveConfig wave)
    {
        this.wave = wave;
        curTime = 0;
        createCount = 0;
    }
    
    public bool Update()
    {
        if ((curTime += Time.deltaTime) >= wave.interval)
        {
            curTime -= wave.interval;
            createCount++;
            Vector2 pos = wave.route.wayPoints[0];
            EnemyManager.Instance.Spawn(wave, pos);
        }

        return createCount >= wave.count; // 生成足够数量之后就不再生成了
    }
}