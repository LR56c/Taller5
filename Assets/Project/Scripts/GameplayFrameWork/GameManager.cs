using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum ERoundWinCondition { CompleteTargetWinCount}

namespace Taller
{

    public class GameManager : Singleton<GameManager>
    {
        [Header("Round Status")]
        public ERoundWinCondition RoundWinCondition;
        public int currentRound;
        public int TargetWinCount = 1;
        [ReadOnly]
        public int currentWinCount;
        [Header("Score Units")]
        public int score;

        public int DragCount = 0;
        public int[] Stars = new int[3];

        [Header("Game events")]
        public EGameStates gameStates = EGameStates.GAMEPLAY;

        public UnityEvent                                OnGameOver,    OnRoundOver, OnRoundBegin;
        public static event FNotify_1Params<int>         OnScoreChange, OnTargetWinCountChange;
        public static event FNotify_1Params<EGameStates> OnGameStateChange;
        

#if UNITY_EDITOR
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                UnityEditor.EditorApplication.isPaused = true;
            }
        }
#endif
        
        public void LoadNextLevel()
        {
            int actualScene = SceneManager.GetActiveScene().buildIndex;
            int maxScene = SceneManager.sceneCountInBuildSettings;

            if(actualScene == (maxScene - 1))
            {
                //mensaje
                SceneManager.LoadSceneAsync(0);
            }
            else
            {
                SceneManager.LoadSceneAsync(actualScene + 1);
            }
        }

        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadSceneAsync(levelIndex);
        }

        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void SetGameOver()
        {
            ChangeGameState(EGameStates.GAME_OVER);
          
            OnGameOver.Invoke();
        }
        
        public void SetRoundOver()
        {
            ChangeGameState(EGameStates.ROUND_OVER); 
            OnRoundOver.Invoke();
        }
        
        public void SetRoundBegin()
        {
            ChangeGameState(EGameStates.GAMEPLAY);
            OnRoundBegin.Invoke();
        }
       
        public void AddScore(int ScoreToAdd)
        {
            score += ScoreToAdd;
            OnScoreChange?.Invoke(score);
        }

        public void ChangeGameState(EGameStates NewGameState)
        {
            gameStates = NewGameState;
            OnGameStateChange?.Invoke(gameStates);

            switch(gameStates)
            {
                case EGameStates.MAIN_MENU:
                    break;
                case EGameStates.GAMEPLAY:
                    break;
                case EGameStates.ROUND_OVER:
                    break;
                case EGameStates.GAME_OVER:
                    break;
            }
        }
        
        public void AddWinCount(int WinValue=1)
        {
            if (gameStates != EGameStates.GAMEPLAY) return;

            currentWinCount += WinValue;
            OnTargetWinCountChange?.Invoke(currentWinCount);

            if(currentWinCount>=TargetWinCount && RoundWinCondition == ERoundWinCondition.CompleteTargetWinCount)
            {
                SetRoundOver();
            }
        }
    }
}

