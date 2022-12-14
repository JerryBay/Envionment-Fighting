/// <summary>
/// 游戏事件
/// </summary>
public enum GameEvent
{
    GameStageUpdate, // 游戏阶段更新
    MoneyUpdate, // 货币更新
    GameTimeUpdate, // 游戏时间更新
    MonsterReadyCreate, // 怪物准备生成

    UI_ProductivityUpdate, // 生产力ui更新
    UI_PollutionUpdate, // 污染情况ui更新
    UI_PollutionValueUpdate, // 污染值ui更新
    UI_WelfareUpdate, // 生活水准ui更新
    UI_ManCountUpdate, // 人数更新
    UI_DeathManCountUpdate, // 死亡人数更新

    UI_OpenBuildingMenu, // 打开建筑菜单
    UI_CloseBuildingMenu, // 关闭建筑分组
    UI_BuildingMenuTypeChanged, // 建筑菜单分组切换
    UI_SelectBuildingPlacePositionStart, // 开始选择建筑放置的位置
    UI_SelectBuildingPlacePositionStop, // 停止选择建筑放置的位置
    UI_SelectBuildingPlacePositionConfirm, // 选择建筑放置的位置确定
    UI_SelectPositionPlaceBuildingStart, // 开始选择位置放置的建筑
    UI_SelectPositionPlaceBuildingStop, // 停止选择位置放置的建筑
    UI_SelectPositionPlaceBuildingUpdate, // 选择位置放置的建筑更新
    UI_SelectPositionPlaceBuildingConfirm, // 选择位置放置的建筑确定
    UI_BuildingUpgrade, // 升级建筑
    UI_BuildingRemove, // 拆除建筑
    UI_BuildingUpgradeComplate, // 建筑升级成功
    UI_BuildingRemoveComplate, // 建筑拆除成功
}

/// <summary>
/// 游戏阶段
/// </summary>
public enum GameStage : sbyte
{
    None = -1,
    MainMenu = 0, // 主菜单
    Playing, // 正在玩
    Success, // 成功
    Failure, // 失败
}

public enum TimeStage : byte
{
    Cultivation = 0, // 农耕时代
    Machine, // 机械时代
    Information, // 信息时代
}

/// <summary>
/// 建筑类型
/// </summary>
public enum BuildingType : byte
{
    Production = 0, // 生产建筑
    Defense = 1, // 防御建筑
}

/// <summary>
/// 波次类型
/// </summary>
public enum WaveType : byte
{
    Normal = 0, // 正常
    Pollute, // 使用污染度
}