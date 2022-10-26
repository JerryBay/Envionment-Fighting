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
        {(int) GameStage.Success, new[] {"UIPanelMain"}},
        {(int) GameStage.Failure, new[] {"UIPanelMain"}},
    };
}