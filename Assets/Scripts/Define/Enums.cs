/// <summary>
/// 游戏事件
/// </summary>
public enum GameEvent
{
    GameStageUpdate, // 游戏阶段更新
    
    UI_ProductivityUpdate, // 生产力ui更新
    UI_PollutionUpdate, // 污染情况ui更新
    UI_WelfareUpdate, // 生活水准ui更新
}

/// <summary>
/// 游戏阶段
/// </summary>
public enum GameStage : byte
{
    MainMenu, // 主菜单
    Playing, // 正在玩
    Success, // 成功
    Failure, // 失败
}