using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using Taller;
using UnityEngine;

public class UIMainMenuSubCanvas : UISubCanvas
{
    [SerializeField] private GameObject _holdFingerImage;
    [SerializeField] private GameObject _fingerImage;
    
    public bool OnGameplay;
    
    protected override void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch (NewGameState)
        {
            case EGameStates.MAIN_MENU:
                ShowContainer();
                break;
            
            case EGameStates.GAMEPLAY:
                OnGameplay = true;
                HideContainer();
                break;

            default: HideContainer(); break;
        }
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
    }

    private void OnFingerUp(LeanFinger obj)
    {
        if(OnGameplay) return;
        _holdFingerImage.SetActive(true);
        _fingerImage.SetActive(false);
    }
    
    private void OnFingerDown(LeanFinger obj)
    {
        if(OnGameplay) return;
        _holdFingerImage.SetActive(false);
        _fingerImage.SetActive(true);
    }
}
