using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // 此类负责处理游戏场景中的事件

    private BuildingConfig buildingConfig;
    private SpriteRenderer buindingGhostSpr;
    private bool isSelectBuildingPosition; // 是否正在选择建筑放置位置

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UIManager.Inst.OpenUIPanel<UIPanelTip>().ShowTip("啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊");
        }

        if (isSelectBuildingPosition)
        {
            Vector2 pos = GameGlobal.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (GridManager.Inst.DetectGridEnable(pos, out GameObject gridKey))
            {
                pos = gridKey.transform.position;
                buindingGhostSpr.color = new Color(1, 1, 1, 1);
            }
            else
            {
                buindingGhostSpr.color = new Color(1, 0, 0, .4f);
            }

            buindingGhostSpr.transform.position = Vector2.Lerp(buindingGhostSpr.transform.position, pos, 0.5f);

            if (Input.GetMouseButtonDown(0))
            {
                if (gridKey != null)
                {
                    BaseBuilding building = BuildingManager.Instance.Spawn(buildingConfig, pos);
                    GridManager.Inst.BuildingSeize(gridKey, building);
                    EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, true);
                }
            }
            else if (Input.GetMouseButtonDown(1)) // 鼠标右键取消放置
            {
                EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, false);
            }
        }
        else // 木有在放置过程的话就可以点击呼出建筑菜单
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = GameGlobal.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(pos, Vector2.right, 0.001f, 1 << LayerUtility.Click);
                if (hitInfo.collider != null)
                {
                    BaseBuilding building = hitInfo.collider.GetComponentInParent<BaseBuilding>();
                    if (building != null)
                        UIManager.Inst.OpenUIPanel<UIPanelInfo>().ShowBuildingInfo(building);
                }
            }
        }
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
        EventManager.Register(GameEvent.UI_BuildingUpgrade, OnBuildingUpgradeEvent);
        EventManager.Register(GameEvent.UI_BuildingRemove, OnBuildingRemoveEvent);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
        EventManager.Unregister(GameEvent.UI_BuildingUpgrade, OnBuildingUpgradeEvent);
        EventManager.Unregister(GameEvent.UI_BuildingRemove, OnBuildingRemoveEvent);
    }

    private void OnSelectBuildingPlacePositionStartEvent(object[] args)
    {
        buildingConfig = (BuildingConfig) args[0];
        isSelectBuildingPosition = true;
        if (buindingGhostSpr == null)
        {
            var obj = new GameObject($"buindingGhost-{buildingConfig.name}");
            obj.transform.localScale = Vector3.one * 0.5f;
            buindingGhostSpr = obj.AddComponent<SpriteRenderer>();
            buindingGhostSpr.sprite = buildingConfig.icon;
            buindingGhostSpr.sortingOrder = 10;
            buindingGhostSpr.drawMode = SpriteDrawMode.Simple;
            Vector2 pos = GameGlobal.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = pos;
        }
    }

    private void OnSelectBuildingPlacePositionStopEvent(object[] args)
    {
        buildingConfig = default;
        isSelectBuildingPosition = false;
        if (buindingGhostSpr != null)
            GameObject.Destroy(buindingGhostSpr.gameObject);
    }

    private void OnBuildingUpgradeEvent(object[] args)
    {
        BaseBuilding building = args[0] as BaseBuilding;
        if (building != null)
        {
            BuildingConfig nextLevel;
            if (building is ProductionBuilding)
                nextLevel = (building as ProductionBuilding).productionBuildingConfig.nextLevel;
            else if (building is DefenseTower)
                nextLevel = (building as DefenseTower).defenseBuildingConfig.nextLevel;
            else return;

            if (nextLevel == null) // 不能继续升级了
                return;

            // todo 判定货币是否足够
            Vector2 pos = building.transform.position;
            building.DestroySelf(); // 销毁当前的
            if (GridManager.Inst.DetectGridEnable(pos, out GameObject gridKey))
            {
                BaseBuilding b = BuildingManager.Instance.Spawn(buildingConfig, pos);
                GridManager.Inst.BuildingSeize(gridKey, b);
                EventManager.Dispath(GameEvent.UI_BuildingUpgradeComplate);
            }
        }
    }

    private void OnBuildingRemoveEvent(object[] args)
    {
        BaseBuilding building = args[0] as BaseBuilding;
        if (building != null)
        {
            // 拆除建筑不需要任何条件
            building.DestroySelf();
            EventManager.Dispath(GameEvent.UI_BuildingRemoveComplate);
        }
    }
}