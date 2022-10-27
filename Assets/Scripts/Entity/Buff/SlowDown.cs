
using UnityEngine;

public class SlowDown : BuffBase
{
    public BaseEnemy enemy;

    private IntervalTimer _timer;

    public override bool BuffStart()
    {
        if (enemy == null)
        {
            return false;
        }
        else
        {
            if (enemy.slowDown != null)
            {
                SlowDown other = enemy.slowDown;
                if (other.buffLevel <= buffLevel)
                {
                    other.BuffEnd();
                    float duration = data.duration[buffLevel];  
                    _timer = new IntervalTimer(duration);
                    _timer.action = () => { BuffEnd();};
                    enemy.speed *= data.buffValue[buffLevel];
                    enemy.slowDown = this;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                float duration = data.duration[buffLevel];  
                _timer = new IntervalTimer(duration);
                _timer.action = () => { BuffEnd();};
                enemy.speed *= data.buffValue[buffLevel];
                enemy.slowDown = this;
                return true;
            }
        }
    }
    
    public override bool BuffEffect()
    {
        return true;
    }

    public override bool BuffEnd()
    {
        if (enemy == null)
        {
            return false;
        }
        enemy.speed /= data.buffValue[buffLevel];
        return true;
    }

    public override bool BuffUpdate()
    {
        _timer.Update();
        return true;
    }
}
