using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMain : UIPanelBase
{
    [SerializeField] private Animator animator;

    [Header("数据")] [SerializeField] private Image productionPro; // 生产力进度
    [SerializeField] private Text productionText; // 生产力文字
    [SerializeField] private Image pollutionPro; // 污染情况进度
    [SerializeField] private Text pollutionText; // 污染情况文字
    [SerializeField] private Text pollutionValue; // 污染值
    [SerializeField] private Image welfarePro; // 生活水准进度
    [SerializeField] private Text welfareText; // 生活水准文字
    [SerializeField] private Text welfareDeathSpeed; // 污染情况死亡速度
    [SerializeField] private Text money; // 货币
    [SerializeField] private Text man; // 人口
    [SerializeField] private Text deathMan; // 死亡人口
    [SerializeField] private Text difficulty; // 难度

    [Header("核心")] [SerializeField] private Text gameTime; // 游戏时长
    [SerializeField] private Text age; // 所处时代

    private static string[] timeStageNames = new[] {"农耕时代", "机械时代", "信息时代"};

    private void Update()
    {
        // // 测试效果
        // productionPro.fillAmount = Mathf.Abs(Mathf.Sin(Time.time));
        // productionText.text = $"{(int) (productionPro.fillAmount * 7f)}/s";
        // pollutionPro.fillAmount = Mathf.Abs(Mathf.Cos(Time.time + Mathf.PI / 3));
        // pollutionText.text = $"{(int) (pollutionPro.fillAmount * 36f)}/s";
        // welfarePro.fillAmount = Mathf.Abs(Mathf.Sin(Time.time + Mathf.PI / 6));
        // welfareText.text = $"{(int) (welfarePro.fillAmount * 86f)}/s";
        // welfareDeathSpeed.text = $"{(int) ((1 - welfarePro.fillAmount) * 10f)}/s";
    }

    protected override void OnShow()
    {
        // 注册ui事件
        EventManager.Register(GameEvent.GameTimeUpdate, OnGameTimeUpdateEvent);
        EventManager.Register(GameEvent.MoneyUpdate, OnMoneyUpdateEvent);
        EventManager.Register(GameEvent.UI_ProductivityUpdate, OnProductivityUpdateEvent);
        EventManager.Register(GameEvent.UI_PollutionUpdate, OnPollutionUpdateEvent);
        EventManager.Register(GameEvent.UI_PollutionValueUpdate, OnPollutionValueUpdateEvent);
        EventManager.Register(GameEvent.UI_WelfareUpdate, OnWelfareUpdateEvent);
        EventManager.Register(GameEvent.UI_ManCountUpdate, OnManCountUpdateEvent);
        EventManager.Register(GameEvent.UI_DeathManCountUpdate, OnDeathManCountUpdateEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);

        animator.Play("TempShow", 0, 1);
    }

    protected override void OnHide()
    {
        // 注销ui事件
        EventManager.Unregister(GameEvent.GameTimeUpdate, OnGameTimeUpdateEvent);
        EventManager.Unregister(GameEvent.MoneyUpdate, OnMoneyUpdateEvent);
        EventManager.Unregister(GameEvent.UI_ProductivityUpdate, OnProductivityUpdateEvent);
        EventManager.Unregister(GameEvent.UI_PollutionUpdate, OnPollutionUpdateEvent);
        EventManager.Unregister(GameEvent.UI_PollutionValueUpdate, OnPollutionValueUpdateEvent);
        EventManager.Unregister(GameEvent.UI_WelfareUpdate, OnWelfareUpdateEvent);
        EventManager.Unregister(GameEvent.UI_ManCountUpdate, OnManCountUpdateEvent);
        EventManager.Unregister(GameEvent.UI_DeathManCountUpdate, OnDeathManCountUpdateEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
    }

    private void OnGameTimeUpdateEvent(object[] args)
    {
        float time = DataManager.Instance.gameTime;
        string minute = Mathf.FloorToInt(time / 60).ToString();
        if (minute.Length == 1) minute = minute.Insert(0, "0");
        string second = Mathf.FloorToInt(time % 60).ToString();
        if (second.Length == 1) second = second.Insert(0, "0");
        gameTime.text = $"{minute}:{second}";
        age.text = timeStageNames[(int) DataManager.Instance.timeStage];
    }

    private void OnMoneyUpdateEvent(object[] args)
    {
        money.text = args[0].ToString();
    }

    private void OnProductivityUpdateEvent(object[] args)
    {
        productionText.text = $"{args[0].ToString()}/s";
    }

    private void OnPollutionUpdateEvent(object[] args)
    {
        pollutionText.text = $"{args[0].ToString()}/s";
    }

    private void OnPollutionValueUpdateEvent(object[] args)
    {
        pollutionValue.text = args[0].ToString();
    }

    private void OnWelfareUpdateEvent(object[] args)
    {
        welfareText.text = ((float) args[0]).ToString("f2");
    }

    private void OnManCountUpdateEvent(object[] args)
    {
        man.text = args[0].ToString();
    }

    private void OnDeathManCountUpdateEvent(object[] args)
    {
        deathMan.text = args[0].ToString();
    }

    private void OnSelectBuildingPlacePositionStartEvent(object[] args)
    {
        animator.Play("TempHide");
    }

    private void OnSelectBuildingPlacePositionStopEvent(object[] args)
    {
        animator.Play("TempShow");
    }

    public void OnBuildButtonClick()
    {
        EventManager.Dispath(GameEvent.UI_OpenBuildingMenu);
    }

    public void OnBuildMenuCloseButtonClick()
    {
        EventManager.Dispath(GameEvent.UI_CloseBuildingMenu);
    }
}