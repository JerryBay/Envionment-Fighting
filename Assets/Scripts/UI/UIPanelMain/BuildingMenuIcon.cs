using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuildingMenuIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    private BuildingConfig building;
    private int index;

    // 初始化建筑图标
    public void Init(BuildingConfig building, int index)
    {
        this.building = building;
        this.index = index;

        icon.sprite = building.icon;

        RefreshAvailable();
    }

    // 刷新图标可用状态
    private bool RefreshAvailable()
    {
        // todo 需要改成数值判定
        // button.interactable = GlobalData.economy >= building.economy; // 检查所需货币是否足够
        button.interactable = true;
        return button.interactable;
    }

    public void OnClick()
    {
        if (!RefreshAvailable())
            return; // 无法放置
        EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStart, building);
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.MoneyUpdate, OnEconomyUpdateEvent);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.MoneyUpdate, OnEconomyUpdateEvent);
    }

    private void OnEconomyUpdateEvent(object[] args)
    {
        RefreshAvailable();
    }
}