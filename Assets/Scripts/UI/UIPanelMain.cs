using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPanelMain : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI productivity;
    [SerializeField] private TextMeshProUGUI pollution;
    [SerializeField] private TextMeshProUGUI welfare;

    protected override void OnShow()
    {
        // 注册ui事件
        EventManager.Register(GameEvent.UI_ProductivityUpdate, OnProductivityUpdateEvent);
        EventManager.Register(GameEvent.UI_PollutionUpdate, OnPollutionUpdateEvent);
        EventManager.Register(GameEvent.UI_WelfareUpdate, OnWelfareUpdateEvent);
    }

    protected override void OnHide()
    {
        // 注销ui事件
        EventManager.Unregister(GameEvent.UI_ProductivityUpdate, OnProductivityUpdateEvent);
        EventManager.Unregister(GameEvent.UI_PollutionUpdate, OnPollutionUpdateEvent);
        EventManager.Unregister(GameEvent.UI_WelfareUpdate, OnWelfareUpdateEvent);
    }

    // 更新生产力
    private void OnProductivityUpdateEvent(object[] args)
    {
        productivity.text = args[0].ToString();
    }

    // 更新污染情况
    private void OnPollutionUpdateEvent(object[] args)
    {
        pollution.text = args[0].ToString();
    }

    // 更新生活水准
    private void OnWelfareUpdateEvent(object[] args)
    {
        welfare.text = args[0].ToString();
    }
}