using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDef
{
    // 游戏配置文件
    public static GameConfig gameConfig;
    
    // 不同阶段时需要创建的ui
    public static Dictionary<int, string[]> stagePanels = new Dictionary<int, string[]>
    {
        {(int) GameStage.MainMenu, new string[] {"UIPanelMainMenu"}},
        {(int) GameStage.Playing, new[] {"UIPanelMain"}},
        {(int) GameStage.Success, new[] {"UIPanelMainMenu"}},
        {(int) GameStage.Failure, new[] {"UIPanelMainMenu"}},
    };
    
    // 格子单位大小
    public static Vector2 gridSize => new Vector2(1.12f, 1.12f);
}