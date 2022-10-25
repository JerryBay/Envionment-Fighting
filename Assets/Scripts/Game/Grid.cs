using System.Collections.Generic;
using UnityEngine;

public struct Grid
{
    public GridType type;

    public GameObject tile;

    public Vector2 position;

    public void SetGridType(GridType type)
    {
        this.type = type;
    }

    public GridType GetGridType()
    {
        return type;
    }

    public void SetPos(Vector2 pos)
    {
        position = pos;
    }

    public Vector2 GetPos()
    {
        return position;
    }
}

public enum GridType
{
    BuildingArea,
    RoadArea
}