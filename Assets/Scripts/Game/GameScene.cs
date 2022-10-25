using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    // 此类负责处理游戏场景中的事件

    private BuildingConfig buildingConfig;
    private GameObject buindingGhost;
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
            buindingGhost.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                // todo 现在是鼠标点击直接取消放置
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
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
    }

    private void OnSelectBuildingPlacePositionStartEvent(object[] args)
    {
        buildingConfig = (BuildingConfig) args[0];
        isSelectBuildingPosition = true;
        buindingGhost = new GameObject($"buindingGhost-{buildingConfig.name}");
        var spriteRenderer = buindingGhost.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = buildingConfig.icon;
        spriteRenderer.sortingOrder = 10;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(1, 1);
    }

    private void OnSelectBuildingPlacePositionStopEvent(object[] args)
    {
        buildingConfig = default;
        isSelectBuildingPosition = false;
        if (buindingGhost != null)
            GameObject.Destroy(buindingGhost);
    }
}