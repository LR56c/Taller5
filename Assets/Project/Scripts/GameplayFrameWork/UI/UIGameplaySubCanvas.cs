using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplaySubCanvas : UISubCanvas
{
    protected override void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch (NewGameState)
        {
            case EGameStates.GAMEPLAY:
                ShowContainer();
                break;

            default: HideContainer(); break;
        }
    }
}
