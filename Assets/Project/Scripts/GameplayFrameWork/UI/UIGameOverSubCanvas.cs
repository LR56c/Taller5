using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using Taller;
using UnityEngine;

public class UIGameOverSubCanvas : UISubCanvas
{
    public  bool        bClicked;
    public  bool        bCanRestart;
    private AudioSource source;

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LeanTouch.OnFingerDown += OnFingerDown;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        LeanTouch.OnFingerDown -= OnFingerDown;
    }
    
    private void OnFingerDown(LeanFinger obj)
    {
        if(obj.IsOverGui) return;
        RestartGame();
    }
    
    private void RestartGame()
    {
        if(!bCanRestart)return;
        if (bClicked) return;
        bClicked = true;
        GameManager.Instance.RestartGame();
    }

    protected override void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch (NewGameState)
        {
            case EGameStates.GAME_OVER:
                ShowContainer();
                source.PlayOneShot(source.clip);
                bCanRestart = true;
                break;

            default: HideContainer(); break;
        }
    }
}
