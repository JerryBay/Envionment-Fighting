using UnityEngine;

public class Timer
{
    private float _startTime;
    private float _duration;
    private bool _isPaused;

    public void InitTime()
    {
        _startTime = Time.time;
        _isPaused = false;
        _duration = 0;
    }

    public void Pause()
    {
        _isPaused = true;
        _duration += Time.time - _startTime;
    }

    public void Play()
    {
        _isPaused = false;
        _startTime = Time.time;
    }

    public bool IsPausedOrNot()
    {
        return _isPaused;
    }

    public float GetDuration()
    {
        if (!_isPaused)
        {
            return _duration + Time.time - _startTime;
        }
        else
        {
            return _duration;
        }
    }
}
