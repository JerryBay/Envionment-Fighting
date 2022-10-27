using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMainBuildingMenu : MonoBehaviour
{
    [SerializeField] private Animator buildAnimator;
    [SerializeField] private Button productionBtn;
    [SerializeField] private Button defenseBtn;
    [SerializeField] private Transform buildingIconTr;
    [SerializeField] private GameObject buildingIconPrefab;

    private bool containsGrid;
    private BuildingType lastShowBuildingType;

    private void Awake()
    {
        // 默认显示生产类型的建筑
        lastShowBuildingType = BuildingType.Production;
        containsGrid = false;
    }

    // 刷新建筑图标
    private void RefreshBuildingsIcon(BuildingType tp, bool containsGrid)
    {
        // 设置按钮可点击状态
        productionBtn.interactable = !(tp == BuildingType.Production);
        defenseBtn.interactable = !(tp == BuildingType.Defense);

        // 销毁当前全部建筑图标（懒得优化了）
        for (int i = buildingIconTr.childCount - 1; i >= 0; i--)
            GameObject.Destroy(buildingIconTr.GetChild(i).gameObject);

        int ind = (int) tp;
        if (GameDef.gameConfig.buildings.Length <= ind)
            return; // 没有可以创建的建筑
        BuildingConfig[] buildings = GameDef.gameConfig.buildings[ind].buildings;

        BuildingMenuIcon btn;
        GameObject go;
        for (int i = 0; i < buildings.Length; i++)
        {
            go = GameObject.Instantiate(buildingIconPrefab, buildingIconTr);
            btn = go.GetComponent<BuildingMenuIcon>();
            btn.Init(buildings[i], i, containsGrid);
            go.SetActive(true);
        }

        this.containsGrid = containsGrid;
        lastShowBuildingType = tp;
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.UI_OpenBuildingMenu, OnOpenBuildingMenuEvent);
        EventManager.Register(GameEvent.UI_CloseBuildingMenu, OnCloseBuildingMenuEvent);
        EventManager.Register(GameEvent.UI_BuildingMenuTypeChanged, OnBuildingMenuTypeChangedEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingStart, OnSelectPositionPlaceBuildingStartEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingStop, OnSelectPositionPlaceBuildingStopEvent);

        buildAnimator.Play("HideBuildingMenu", 0, 1);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.UI_OpenBuildingMenu, OnOpenBuildingMenuEvent);
        EventManager.Unregister(GameEvent.UI_CloseBuildingMenu, OnCloseBuildingMenuEvent);
        EventManager.Unregister(GameEvent.UI_BuildingMenuTypeChanged, OnBuildingMenuTypeChangedEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
    }

    private void OnOpenBuildingMenuEvent(object[] args)
    {
        RefreshBuildingsIcon(lastShowBuildingType, false);
        buildAnimator.Play("ShowBuildingMenu");
    }

    private void OnCloseBuildingMenuEvent(object[] args)
    {
        buildAnimator.Play("HideBuildingMenu");
        if(containsGrid)
            EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingStop, false);
    }

    private void OnBuildingMenuTypeChangedEvent(object[] args)
    {
        BuildingType tp = (BuildingType) args[0];
        RefreshBuildingsIcon(tp, containsGrid);
    }

    private void OnSelectBuildingPlacePositionStartEvent(object[] args)
    {
        buildAnimator.Play("TempHide");
    }

    private void OnSelectBuildingPlacePositionStopEvent(object[] args)
    {
        RefreshBuildingsIcon(lastShowBuildingType, false);
        if ((bool) args[0])
            buildAnimator.Play("TempoShowOnlyButton");
        else buildAnimator.Play("TempShow");
    }

    private void OnSelectPositionPlaceBuildingStartEvent(object[] args)
    {
        RefreshBuildingsIcon(lastShowBuildingType, true);
        buildAnimator.Play("ShowBuildingMenu");
    }

    private void OnSelectPositionPlaceBuildingStopEvent(object[] args)
    {
        RefreshBuildingsIcon(lastShowBuildingType, false);
        buildAnimator.Play("HideBuildingMenu");
    }

    public void OnProductionButtonClick()
    {
        EventManager.Dispath(GameEvent.UI_BuildingMenuTypeChanged, BuildingType.Production);
    }

    public void OnDefenseButtonClick()
    {
        EventManager.Dispath(GameEvent.UI_BuildingMenuTypeChanged, BuildingType.Defense);
    }
}