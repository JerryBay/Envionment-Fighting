using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BuildingMenuIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text name;
    [SerializeField] private Image blackImg;

    private BuildingConfig building;
    private int index;
    private bool isDrag;
    private bool containsGrid;

    // 初始化建筑图标
    public void Init(BuildingConfig building, int index, bool containsGrid)
    {
        this.building = building;
        this.index = index;
        this.containsGrid = containsGrid;

        icon.sprite = building.icon;
        name.text = building.name;

        RefreshAvailable();
    }

    // 刷新图标可用状态
    private bool RefreshAvailable()
    {
        // todo 需要改成数值判定
        // button.interactable = GlobalData.economy >= building.economy; // 检查所需货币是否足够
        blackImg.gameObject.SetActive(false);
        return true;
    }

    public void OnPointerEnter()
    {
        if (containsGrid)
            EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingUpdate, building);
    }

    public void OnPointerExit()
    {
        if (containsGrid)
            EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingUpdate, new object[] {null,});
    }

    public void OnPointerDragStart()
    {
        if (containsGrid || !RefreshAvailable())
            return;
        isDrag = true;
        EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStart, building);
    }

    public void OnPointerDragStop()
    {
        if (!isDrag || containsGrid || !RefreshAvailable())
            return;
        EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionConfirm);
    }

    public void OnPointerClick()
    {
        if (isDrag || !RefreshAvailable())
            return;

        if (containsGrid)
            EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingConfirm, building);
        else EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStart, building);
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.MoneyUpdate, OnMoneyUpdateEvent);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.MoneyUpdate, OnMoneyUpdateEvent);
    }

    private void OnMoneyUpdateEvent(object[] args)
    {
        RefreshAvailable();
    }
}