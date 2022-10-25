using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : SingletonMono<GridManager>
{
    public float2 size;
    public int2 count;
    
    private Grid[,] _grids;

    private void Awake()
    {
        _grids = new Grid[count.x, count.y];

        for (int i = 0; i < count.x; i++)
        {
            for (int j = 0; j < count.y; j++)
            {
                Vector2 pos = GetPos(new int2(i, j));
                _grids[i,j].SetPos(pos);
            }
        }
    }

    public Vector2 GetPos(int2 index)
    {
        int row = index.x,column = index.y;
        // if (row > xCount || column > yCount || row < 0 || column < 0)
        // {
        //     throw new Exception("out of boundary");
        // }

        float xMid = count.x * 0.5f;
        float yMid = count.y * 0.5f;

        float xPos = (row - xMid + 0.5f) * size.x;
        float yPos = (column - yMid + 0.5f) * size.y;

        return new Vector2(xPos, yPos);
    }

    public int2 GetIndex(Vector2 pos)
    {
        float xPos = pos.x, yPos = pos.y;
        float xMid = count.x * 0.5f;
        float yMid = count.y * 0.5f;

        int xIndex = (int) (xPos / size.x + xMid);
        int yIndex = (int) (yPos / size.y + yMid);

        return new int2(xIndex, yIndex);
    }
}
