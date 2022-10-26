using System;
using System.Collections;
using System.Collections.Generic;
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
            buindingGhostSpr = obj.AddComponent<SpriteRenderer>();
            buindingGhostSpr.sprite = buildingConfig.icon;
            buindingGhostSpr.sortingOrder = 10;
            buindingGhostSpr.drawMode = SpriteDrawMode.Sliced;
            buindingGhostSpr.size = new Vector2(1, 1);
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
            // todo 升级建筑
            Debug.Log("升级建筑");
        }
    }

    private void OnBuildingRemoveEvent(object[] args)
    {
        BaseBuilding building = args[0] as BaseBuilding;
        if (building != null)
        {
            // todo 拆除建筑
            Debug.Log("拆除建筑");
        }
    }
}