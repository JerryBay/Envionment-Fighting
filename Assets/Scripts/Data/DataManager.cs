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
    public float population = 0;
    public float polluteRate = 0;
    public float polluteTotal = 0;
    public float welfare = 0;
    public float peopleDead = 0;
    
    public float gameTime = 0; // 游戏时间
    public TimeStage timeStage; // 时间阶段
    private bool gameStart; // 是否已经开始游戏

    private void OnMinuteStep()
    {
        population = (int) (population * 1.01) + 20;
        EventManager.Dispath(GameEvent.UI_ManCountUpdate,population);
    }

    private void OnSecondStep()
    {
        polluteTotal += polluteRate;
        coin += productivity;
        EventManager.Dispath(GameEvent.UI_PollutionValueUpdate,polluteTotal);
        EventManager.Dispath(GameEvent.MoneyUpdate,coin);

        // 更新游戏时间
        gameTime++;
        DataManager.Instance.timeStage =
            (TimeStage) Mathf.FloorToInt(gameTime / 60f / GameDef.gameConfig.timeStageStepMinute);
        EventManager.Dispath(GameEvent.GameTimeUpdate, gameTime);
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if (gameStart) // 开始游戏后才更新游戏时间
        {
            oneMinute.Update();
            oneSecond.Update();
            welfare = (50 + productivity * 10 - polluteRate * 2) /population * 1;
            EventManager.Dispath(GameEvent.UI_WelfareUpdate, welfare);
        }
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
    }

    private void OnGameStageUpdateEvent(object[] args)
    {
        GameStage stage = (GameStage) args[0];
        switch (stage)
        {
            case GameStage.Playing: // 开始游戏
                timer = new Timer();
                timer.InitTime();
                oneMinute = new IntervalTimer(60);
                oneMinute.action = OnMinuteStep;
                oneSecond = new IntervalTimer(1);
                oneSecond.action = OnSecondStep;

                coin = config.primaryCoin;
                population = config.primaryPopulation;

                gameStart = true;
                gameTime = 0;
                timeStage = TimeStage.Cultivation;
                EventManager.Dispath(GameEvent.GameTimeUpdate, 0);

                EventManager.Dispath(GameEvent.UI_ProductivityUpdate,productivity);
                EventManager.Dispath(GameEvent.UI_PollutionUpdate,polluteRate);
                EventManager.Dispath(GameEvent.UI_ManCountUpdate,population);
                EventManager.Dispath(GameEvent.UI_DeathManCountUpdate,peopleDead);
                EventManager.Dispath(GameEvent.UI_PollutionValueUpdate,polluteTotal);
                break;
            default:
                gameStart = false;
                break;
        }
    }

    public void UpdateProductivity(float value)
    {
        productivity += value;
        EventManager.Dispath(GameEvent.UI_ProductivityUpdate,productivity);
    }
    
    public void UpdatePolluteRate(float value)
    {
        polluteRate += value;
        EventManager.Dispath(GameEvent.UI_PollutionUpdate,polluteRate);
    }

    public void UpdatePopulation(float value)
    {
        if (value < 0)
        {
            if (population + value < 0)
            {
                population = 0;
                UpdateManDead(population);
            }
            else
            {
                population += value;
                UpdateManDead(-value);
            }
        }
        else
        {
            population += value;
        }
        EventManager.Dispath(GameEvent.UI_ManCountUpdate,population);
    }

    public void UpdateManDead(float value)
    {
        peopleDead += value;
        EventManager.Dispath(GameEvent.UI_DeathManCountUpdate,peopleDead);
    }
}