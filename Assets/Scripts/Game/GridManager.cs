using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    public static GridManager Inst { get; } = new GridManager();

    private Dictionary<GameObject, BaseBuilding> buildingsMap = new Dictionary<GameObject, BaseBuilding>();
    private readonly GameObject gridsParent;

    public GridManager()
    {
        gridsParent = new GameObject("Grids Parent");
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        var trs = GameObject.Find("GridsConfig").transform;
        GameObject obj;
        BoxCollider2D col;
        for (int i = 0; i < trs.childCount; i++)
        {
            obj = new GameObject("Grid");
            obj.layer = LayerUtility.Grid;
            obj.transform.SetParent(gridsParent.transform);
            obj.transform.position = trs.GetChild(i).position;
            col = obj.AddComponent<BoxCollider2D>();
            col.size = GameDef.gridSize;
            col.offset = Vector2.zero;
            col.isTrigger = true;
            trs.GetChild(i).gameObject.SetActive(false);
            buildingsMap[obj] = null;
        }
    }

    /// <summary>
    /// 判断格子里是否有指定建筑
    /// </summary>
    public bool IsBuilding(Vector2 worldPos, BaseBuilding building)
    {
        return PosToGridKey(worldPos, out GameObject gridKey) && buildingsMap[gridKey] == building;
    }

    private bool PosToGridKey(Vector2 worldPos, out GameObject gridKey)
    {
        gridKey = null;
        RaycastHit2D hitInfo = Physics2D.Raycast(worldPos, Vector2.right, 0.001f, 1 << LayerUtility.Grid);
        if (hitInfo.collider == null || !buildingsMap.ContainsKey(hitInfo.collider.gameObject))
            return false;
        gridKey = hitInfo.collider.gameObject;
        return true;
    }

    /// <summary>
    /// 检查格子是否可用
    /// </summary>
    public bool DetectGridEnable(Vector2 worldPos, out GameObject gridKey)
    {
        gridKey = null;
        RaycastHit2D hitInfo = Physics2D.Raycast(worldPos, Vector2.right, 0.001f, 1 << LayerUtility.Grid);
        if (hitInfo.collider == null)
            return false;

        gridKey = hitInfo.collider.gameObject;
        if (buildingsMap.TryGetValue(gridKey, out BaseBuilding b) && b == null)
        {
            return true;
        }

        gridKey = null;
        return false;
    }

    /// <summary>
    /// 检查格子是否可用
    /// </summary>
    public bool DetectGridEnable(GameObject gridKey)
    {
        return buildingsMap.TryGetValue(gridKey, out BaseBuilding b) && b == null;
    }

    /// <summary>
    /// 建筑占用格子
    /// </summary>
    public bool BuildingSeize(GameObject gridKey, BaseBuilding building)
    {
        if (buildingsMap.TryGetValue(gridKey, out BaseBuilding b) && b == null)
        {
            buildingsMap[gridKey] = building;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 释放建筑所占用的格子
    /// </summary>
    public bool BuildingRelease(BaseBuilding building)
    {
        GameObject gridKey = null;
        foreach (var v in buildingsMap)
        {
            if (v.Value == building)
            {
                gridKey = v.Key;
                break;
            }
        }

        if (gridKey == null)
            return false;

        if (buildingsMap.TryGetValue(gridKey, out BaseBuilding b) && b == building)
        {
            buildingsMap[gridKey] = null;
            return true;
        }

        return false;
    }
}