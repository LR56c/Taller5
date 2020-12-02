using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Taller;
using UnityEngine;

public class UISubCanvas : MonoBehaviour
{
    public bool  defaultShowContainer;
    public float fadeInTime            = 0.2f;
    public float fadeOutTime           = 0.2f;

    [Header("Referencia al container")]
    public GameObject container;
    private CanvasGroup canvasGroup;
    
    protected virtual void Awake()
    {
        FindCanvas();
    }

    protected virtual void Start()
    {
        if(defaultShowContainer ==false) container?.SetActive(false);
    }

    protected void FindCanvas()
    {
        container =transform.GetChild(0).gameObject;
        canvasGroup = container.GetComponent<CanvasGroup>();
    }
    
    protected virtual void OnEnable()
    {
        FindCanvas();
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }
    
    protected virtual void OnDisable()
    {
        GameManager.OnGameStateChange -= GameManager_OnGameStateChange;
    }
    
    protected virtual void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch (NewGameState)
        {
            case EGameStates.ROUND_OVER:
                ShowContainer();
                break;

            default: HideContainer(); break;
        }
    }

    public void ShowContainer()
    {
        container?.SetActive(true);
        //canvasGroup?.DOFade(1, fadeInTime);
        canvasGroup?.LeanAlpha(1, fadeInTime);
    }

    public void HideContainer()
    {
        container?.SetActive(false);
        //canvasGroup?.DOFade(0, fadeOutTime);
        canvasGroup?.LeanAlpha(0, fadeOutTime);
    }
}
