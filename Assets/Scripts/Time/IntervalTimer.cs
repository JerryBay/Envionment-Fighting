
using UnityEngine;
using UnityEngine.Events;

public class IntervalTimer
{
    public float interval;

    private float _startTime;

    public UnityAction action;

    public IntervalTimer(float interval)
    {
        Init(interval);
    }

    public void Init(float interval)
    {
        this.interval = interval;
        _startTime = Time.time;
    }

    public void Update()
    {
        float nowTime = Time.time;
        if (nowTime - _startTime >= interval)
        {
            action.Invoke();
            _startTime = nowTime;
        }
    }

    public float GetTime()
    {
        return Time.time - _startTime;
    }
}
