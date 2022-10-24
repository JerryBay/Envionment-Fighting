using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public UIManager uiMgr;
    private DataManager _globalData;

    public void UpdateData()
    {
    }

    public void UpdateUI()
    {
    }

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