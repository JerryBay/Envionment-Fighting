using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    protected virtual void Awake()
    {
        if (transform.Find("ClickBox") == null)
        {
            GameObject obj = new GameObject("ClickBox");
            obj.transform.SetParent(transform, false);
            obj.layer = LayerUtility.Click;
            BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = GameDef.gridSize;
        }
    }

    protected virtual void OnDestroy()
    {
    }

    // 销毁该建筑
    public void DestroySelf()
    {
        GridManager.Inst.BuildingRelease(this);
        GameObject.Destroy(gameObject);
    }
}