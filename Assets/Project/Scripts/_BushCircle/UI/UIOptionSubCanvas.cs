using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Taller;
using UnityEngine.UI;

public class UIOptionSubCanvas : UISubCanvas
{
    public  GameObject  secondCountainer;
    private CanvasGroup secondCanvasGroup;
    public  int         _projectSceneCount;

    private InputField field;
    
    protected override void Awake()
    {
        base.Awake();
        _projectSceneCount = SceneManager.sceneCountInBuildSettings - 1;
        secondCountainer = transform.GetChild(1).gameObject;
        secondCanvasGroup = secondCountainer.GetComponent<CanvasGroup>();
    }
    
    public void TestString(string test)
    {
        int toInt = 0;
        
        toInt = int.Parse(test);
        
        if(toInt >= 0 && toInt <= _projectSceneCount)
        {
            GameManager.Instance.LoadLevel(toInt);
        }
        else if(toInt < 0 || toInt > _projectSceneCount)
        {
            GameManager.Instance.LoadLevel(0);
        }
        else
        {
            //Debug.Log("otro: " + toInt);
        }
    }
    

    protected override void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch(NewGameState)
        {
            case  EGameStates.GAMEPLAY:
                ShowContainer();
                break;
        }
    }
    

    public void ShowSecondContainer()
    {
        secondCountainer?.SetActive(true);
        //secondCanvasGroup?.DOFade(1, fadeInTime);
        secondCanvasGroup?.LeanAlpha(1, fadeInTime);

    }

    public void HideSecondContainer()
    {
        secondCountainer?.SetActive(false);
        //secondCanvasGroup?.DOFade(0, fadeOutTime);
        secondCanvasGroup?.LeanAlpha(0, fadeOutTime);
    }
}
