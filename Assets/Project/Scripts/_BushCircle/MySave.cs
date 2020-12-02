using System;
using System.Collections;
using System.Collections.Generic;
using Taller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySave : MonoBehaviour
{
    public int _levelSceneIndex = 0;
    public int _projectSceneCount = 0;
    
    private Dictionary<string, bool> MyLevelsUnlocked      = new Dictionary<string, bool>();
    private Dictionary<string, int>  MyStarsLevelsUnlocked = new Dictionary<string, int>();

    private void Start()
    {
        _projectSceneCount = SceneManager.sceneCountInBuildSettings - 1;
        _levelSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        var key = ES3.KeyExists("bPlayed");
        if(key)
        {
            MyLevelsUnlocked = ES3.Load<Dictionary<string, bool>>("LevelsUnlocked");
            MyStarsLevelsUnlocked = ES3.Load<Dictionary<string, int>>("StarsLevelsUnlocked");
            return;
        }
        
        for(int i = 0; i <= _projectSceneCount; i++)
        {
            MyLevelsUnlocked.Add($"Level{i+1}", false);
            MyLevelsUnlocked.Remove($"Level1");
            MyLevelsUnlocked.Add($"Level1", true);
            MyStarsLevelsUnlocked.Add($"Level{i +1}", 0);
        }

        ES3.Save("LevelsUnlocked",      MyLevelsUnlocked);
        ES3.Save("StarsLevelsUnlocked", MyStarsLevelsUnlocked);
        
        ES3.Save("bPlayed", true);
        SceneManager.LoadSceneAsync(1);
        
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(EGameStates param1)
    {
        if(param1 != EGameStates.ROUND_OVER) return;
        SaveLevel();
    }
    
    private void SaveLevel()
    {
        if(_levelSceneIndex == 0) return;
        
        if(MyLevelsUnlocked.ContainsKey($"Level{_levelSceneIndex + 1}"))
        {
            MyLevelsUnlocked.Remove($"Level{_levelSceneIndex + 1}");
        }
        
        MyLevelsUnlocked.Add($"Level{_levelSceneIndex + 1}", true);

        ES3.Save("LevelsUnlocked", MyLevelsUnlocked);
    }
}
