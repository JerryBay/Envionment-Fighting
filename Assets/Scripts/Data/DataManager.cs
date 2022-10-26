using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    public DataConfig config;

    public Timer timer;
    public IntervalTimer oneMinute;
    public IntervalTimer oneSecond;

    public float coin = 0;
    public float productivity = 0;
    public int population = 0;
    public float polluteRate = 0;
    public float polluteTotal = 0;
    public float evaluation = 0;

    public int peopleDead = 0;

    private void Awake()
    {
        timer = new Timer();
        timer.InitTime();
        oneMinute = new IntervalTimer(60);
        oneMinute.action += () => { population = (int)(population * 1.01) + 20;};
        oneSecond = new IntervalTimer(1);
        oneSecond.action += () => { polluteTotal += polluteRate;};
        oneSecond.action += () => { coin += productivity;};
    }

    private void Start()
    {
        coin = config.primaryCoin;
        population = config.primaryPopulation;
        
    }

    private void Update()
    {
        oneMinute.Update();
        oneSecond.Update();
        evaluation = (50 + productivity * 10 - polluteRate * 2) /population * 1;
    }
}
