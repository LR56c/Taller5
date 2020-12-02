using System.Collections;
using System.Collections.Generic;
using Taller;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerListenerComponent : MonoBehaviour
{
    public UnityEvent OnMainMenu,OnGameplayBegin, OnGameOver, OnRoundOver, OnRoundBegin;


    private void OnEnable()
    {
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChange -= GameManager_OnGameStateChange;

    }
    private void GameManager_OnGameStateChange(EGameStates GameState)
    {
 
        switch (GameState)
        {
            case EGameStates.MAIN_MENU:
                OnMainMenu?.Invoke();
                break;
            case EGameStates.CONNECTING:
                break;
            case EGameStates.RELOADING_ROUND:
                break;
            case EGameStates.LOADING_NEXTROUND:
                OnRoundBegin.Invoke();
                break;
            case EGameStates.LOADING_REMATCH:
                break;
            case EGameStates.GAMEPLAY:
                OnGameplayBegin?.Invoke();
                break;
            case EGameStates.ROUND_OVER:
                OnRoundOver.Invoke();
                break;
            case EGameStates.GAME_OVER:
                OnGameOver.Invoke();
                break;
        }

   
    }

   
}