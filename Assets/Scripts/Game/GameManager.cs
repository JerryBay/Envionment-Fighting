using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig; // 游戏配置文件
    
    // 初始化游戏
    private void InitGame()
    {
        GameDef.gameConfig = gameConfig;
        GameGlobal.MainCamera = Camera.main;
        GameGlobal.UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
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