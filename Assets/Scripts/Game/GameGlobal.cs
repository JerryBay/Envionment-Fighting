using UnityEngine;

public static class GameGlobal
{
    public static Camera MainCamera; // 主相机
    public static Camera UICamera; // ui相机
    public static GameStage GameStage = GameStage.None; // 当前游戏阶段

    public static bool IsPause // 是否处于暂停中
    {
        get => pauseCount > 0;
        set
        {
            pauseCount += value ? 1 : -1;
            Time.timeScale = pauseCount > 0 ? 0 : 1;
        }
    }
    private static int pauseCount;
}