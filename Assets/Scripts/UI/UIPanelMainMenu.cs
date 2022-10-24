using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelMainMenu : UIPanelBase
{
    public void OnStartGameButtonClick()
    {
        EventManager.Dispath(GameEvent.GameStageUpdate, GameStage.Playing);
    }
}