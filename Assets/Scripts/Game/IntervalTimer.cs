using UnityEngine;

public class IntervalTimer
{
    private float _startTime;
    private float _duration;
    private bool _isPaused;

    public void InitTime()
    {
        _startTime = Time.time;
        _duration = 0;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Play()
    {
        _isPaused = true;
    }

    public bool IsPausedOrNot()
    {
        return _isPaused;
    }

    public void Update()
    {
        if (!_isPaused)
        {
            _duration += Time.deltaTime;
        }
    }

    public float GetDuration()
    {
        return _duration;
    }
}
