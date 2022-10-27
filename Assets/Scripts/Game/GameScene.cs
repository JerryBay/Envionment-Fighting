using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameScene : MonoBehaviour
{
    // 此类负责处理游戏场景中的事件

    private BuildingConfig buildingConfig;
    private GameObject buildingSelectGrid;
    private SpriteRenderer buindingGhostSpr;
    private bool isSelectBuildingPosition; // 是否正在选择建筑要放置的位置
    private bool isSelectPositionBuilding; // 是否正在选择位置要放置的建筑

    private WaveCreator waveCreator;
    private bool gameStart;
    private TimeStage lastTimeStage;

    // private void OnGUI()
    // {
    //     GUILayout.Label($"isSelectBuildingPosition:{isSelectBuildingPosition}");
    //     GUILayout.Label($"buildingSelectGrid:{buildingSelectGrid}");
    // }

    public void Awake()
    {
        waveCreator = new WaveCreator();
    }

    private void Update()
    {
        if (!gameStart) return;

        if (Input.GetKeyDown(KeyCode.End))
            Time.timeScale = 50;
        if (Input.GetKeyUp(KeyCode.End))
            Time.timeScale = 1;

        waveCreator.Update();
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
                if (TryBuilding(pos, buildingConfig))
                {
                    EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, true);
                }
            }
            else if (Input.GetMouseButtonDown(1)) // 鼠标右键取消放置
            {
                EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, false);
            }
        }
        else if (!isSelectPositionBuilding &&
                 !EventSystem.current.IsPointerOverGameObject()) // 木有在放置过程的话就可以点击呼出建筑菜单
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
                else
                {
                    hitInfo = Physics2D.Raycast(pos, Vector2.right, 0.001f, 1 << LayerUtility.Grid);
                    if (hitInfo.collider != null)
                    {
                        if (GridManager.Inst.DetectGridEnable(hitInfo.collider.gameObject))
                            EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingStart,
                                hitInfo.collider.gameObject);
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        EventManager.Register(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
        EventManager.Register(GameEvent.GameTimeUpdate, OnGameTimeUpdateEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
        EventManager.Register(GameEvent.UI_SelectBuildingPlacePositionConfirm,
            OnSelectBuildingPlacePositionConfirmEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingStart, OnSelectPositionPlaceBuildingStartEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingStop, OnSelectPositionPlaceBuildingStopEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingUpdate, OnSelectPositionPlaceBuildingUpdateEvent);
        EventManager.Register(GameEvent.UI_SelectPositionPlaceBuildingConfirm,
            OnSelectPositionPlaceBuildingConfirmEvent);
        EventManager.Register(GameEvent.UI_BuildingUpgrade, OnBuildingUpgradeEvent);
        EventManager.Register(GameEvent.UI_BuildingRemove, OnBuildingRemoveEvent);
    }

    private void OnDisable()
    {
        EventManager.Unregister(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
        EventManager.Unregister(GameEvent.GameTimeUpdate, OnGameTimeUpdateEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStart, OnSelectBuildingPlacePositionStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionStop, OnSelectBuildingPlacePositionStopEvent);
        EventManager.Unregister(GameEvent.UI_SelectBuildingPlacePositionConfirm,
            OnSelectBuildingPlacePositionConfirmEvent);
        EventManager.Unregister(GameEvent.UI_SelectPositionPlaceBuildingStart, OnSelectPositionPlaceBuildingStartEvent);
        EventManager.Unregister(GameEvent.UI_SelectPositionPlaceBuildingStop, OnSelectPositionPlaceBuildingStopEvent);
        EventManager.Unregister(GameEvent.UI_SelectPositionPlaceBuildingUpdate,
            OnSelectPositionPlaceBuildingUpdateEvent);
        EventManager.Unregister(GameEvent.UI_SelectPositionPlaceBuildingConfirm,
            OnSelectPositionPlaceBuildingConfirmEvent);
        EventManager.Unregister(GameEvent.UI_BuildingUpgrade, OnBuildingUpgradeEvent);
        EventManager.Unregister(GameEvent.UI_BuildingRemove, OnBuildingRemoveEvent);
    }

    private void OnGameStageUpdateEvent(object[] args)
    {
        GameStage stage = (GameStage) args[0];
        switch (stage)
        {
            case GameStage.Playing: // 开始游戏
                gameStart = true;
                lastTimeStage = TimeStage.Cultivation;
                waveCreator.Reset(GameDef.gameConfig.timeStageWaves[0], lastTimeStage);
                break;
            default:
                gameStart = false;
                break;
        }
    }

    private void OnGameTimeUpdateEvent(object[] args)
    {
        if (lastTimeStage == DataManager.Instance.timeStage)
            return;
        lastTimeStage = DataManager.Instance.timeStage;
        if (GameDef.gameConfig.timeStageWaves.Length > (int) lastTimeStage)
            waveCreator.Reset(GameDef.gameConfig.timeStageWaves[(int) lastTimeStage], lastTimeStage);
    }

    private void OnSelectBuildingPlacePositionStartEvent(object[] args)
    {
        buildingConfig = (BuildingConfig) args[0];
        buildingSelectGrid = null;
        isSelectBuildingPosition = true;

        Vector2 pos = GameGlobal.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        CreateBuildingGhost(buildingConfig, pos);
    }

    private void OnSelectBuildingPlacePositionStopEvent(object[] args)
    {
        buildingConfig = null;
        isSelectBuildingPosition = false;
        DestroyBuildingGhost();
    }

    private void OnSelectBuildingPlacePositionConfirmEvent(object[] args)
    {
        if (!isSelectBuildingPosition)
            return;
        Vector2 pos = GameGlobal.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (TryBuilding(pos, buildingConfig))
        {
            EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, true);
        }
        else
        {
            EventManager.Dispath(GameEvent.UI_SelectBuildingPlacePositionStop, false);
        }
    }

    private void OnSelectPositionPlaceBuildingStartEvent(object[] args)
    {
        buildingSelectGrid = args[0] as GameObject;
        isSelectPositionBuilding = true;
    }

    private void OnSelectPositionPlaceBuildingStopEvent(object[] args)
    {
        buildingSelectGrid = null;
        DestroyBuildingGhost();
        isSelectPositionBuilding = false;
    }

    private void OnSelectPositionPlaceBuildingUpdateEvent(object[] args)
    {
        if (!isSelectPositionBuilding) return;
        DestroyBuildingGhost();

        if (args[0] != null)
        {
            BuildingConfig config = args[0] as BuildingConfig;
            CreateBuildingGhost(config, buildingSelectGrid.transform.position);

            if (GridManager.Inst.DetectGridEnable(buildingSelectGrid))
                buindingGhostSpr.color = new Color(1, 1, 1, 1);
            else buindingGhostSpr.color = new Color(1, 0, 0, .4f);
        }
    }

    private void OnSelectPositionPlaceBuildingConfirmEvent(object[] args)
    {
        if (!isSelectPositionBuilding) return;

        BuildingConfig building = args[0] as BuildingConfig;
        if (building != null)
        {
            if (TryBuilding(buildingSelectGrid, building))
            {
                buildingSelectGrid = null;
                EventManager.Dispath(GameEvent.UI_SelectPositionPlaceBuildingStop, true);
            }
        }
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
                BaseBuilding b = BuildingManager.Instance.Spawn(nextLevel, pos);
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

    // 创建建筑残影
    private void CreateBuildingGhost(BuildingConfig config, Vector2 pos)
    {
        DestroyBuildingGhost();

        var obj = new GameObject($"buindingGhost-{config.name}");
        buindingGhostSpr = obj.AddComponent<SpriteRenderer>();
        buindingGhostSpr.sprite = config.icon;
        buindingGhostSpr.sortingOrder = 10;
        buindingGhostSpr.drawMode = SpriteDrawMode.Sliced;
        buindingGhostSpr.size = GameDef.gridSize;
        obj.transform.position = pos;
    }

    // 销毁建筑残影
    private void DestroyBuildingGhost()
    {
        if (buindingGhostSpr != null)
        {
            GameObject.Destroy(buindingGhostSpr.gameObject);
            buindingGhostSpr = null;
        }
    }

    // 尝试建造建筑
    private bool TryBuilding(Vector2 pos, BuildingConfig config)
    {
        // todo 判定货币是否足够

        if (GridManager.Inst.DetectGridEnable(pos, out GameObject gridKey))
        {
            BaseBuilding building = BuildingManager.Instance.Spawn(buildingConfig, gridKey.transform.position);
            GridManager.Inst.BuildingSeize(gridKey, building);
            return true;
        }

        return false;
    }

    // 尝试建造建筑
    private bool TryBuilding(GameObject gridKey, BuildingConfig config)
    {
        // todo 判定货币是否足够

        if (GridManager.Inst.DetectGridEnable(gridKey))
        {
            BaseBuilding building = BuildingManager.Instance.Spawn(config, gridKey.transform.position);
            GridManager.Inst.BuildingSeize(gridKey, building);
            return true;
        }

        return false;
    }
}