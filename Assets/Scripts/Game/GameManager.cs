using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 初始化游戏
    private void InitGame()
    {
        UIManager.Inst.Init();
    }

    // 开始游戏
    public void StartGame()
    {
        EventManager.Dispath(GameEvent.GameStageUpdate, GameStage.MainMenu);
    }

    private void Awake()
    {
        InitGame();
    }

    private void Start()
    {
        StartGame();
    }
}