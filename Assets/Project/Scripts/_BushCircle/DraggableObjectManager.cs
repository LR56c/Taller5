using System;
using System.Collections;
using System.Collections.Generic;
using _BushCircle.MyScriptableObject;
using Lean.Touch;
using Taller;
using UnityEngine;

public class DraggableObjectManager : MonoBehaviour
{
    [SerializeField] private DraggableObjectController[] draggableObjects;
    [SerializeField] private int                         _count;
    public                   bool                        bCanDrag;
    private GameObject                  nubeTuto;
    public                   int                         callId;
    public                   float                       callTimer = 15f;

    private void Awake()
    {
        nubeTuto = transform.Find("NubeTutorial").gameObject;
        draggableObjects = GetComponentsInChildren<DraggableObjectController>();
        _count = draggableObjects.Length;
        nubeTuto?.SetActive(false);
    }

    private void OnEnable()
    {
        LeanSelectable.OnSelectUpGlobal += OnSelectUpGlobal;
        LeanSelectable.OnSelectGlobal += OnSelectGlobal;
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(EGameStates param1)
    {
        switch(param1)
        {
            case EGameStates.ROUND_OVER:
            case EGameStates.GAME_OVER:
                bCanDrag = true; 
                return;
        }
    }

    private void OnDisable()
    {
        LeanSelectable.OnSelectUpGlobal -= OnSelectUpGlobal;
        LeanSelectable.OnSelectGlobal -= OnSelectGlobal;
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }

    private void Start()
    {
        SetTuto();
    }

    private void OnSelectGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        if(nubeTuto == null) return;
        SetTuto();
    }

    private void SetTuto()
    {
        CheckCall();
        nubeTuto.SetActive(false);
        SetCall();
    }

    private void OnSelectUpGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        if(nubeTuto == null) return;
        SetTuto();
    }

    private void CheckCall()
    {
        /*var callIsActive = DOTween.IsTweening(callId);
        if(callIsActive)DOTween.Kill(callId);*/

        var callIsActiveLean = LeanTween.isTweening(this.gameObject);
        if(callIsActiveLean) LeanTween.cancel(this.gameObject, callId);
    }
    
    private void SetCall()
    {
        /*var call = DOVirtual.DelayedCall(callTimer, Callback);
        callId = call.intId;*/
        
        var leanCall = LeanTween.delayedCall(this.gameObject, callTimer, (Callback));
        this.callId = leanCall.id;
    }

    private void Callback()
    {
        nubeTuto.SetActive(true);
    }

    private void ChangeState()
    {
        switch(GameManager.Instance.gameStates)
        {
            case EGameStates.ROUND_OVER:
            case EGameStates.MAIN_MENU:
                return;
            
            default: GameManager.Instance.ChangeGameState(EGameStates.GAME_OVER); break;
        }
    }

    public void CheckState()
    {
        _count--;
        if(_count <= 0)
        {
            //DOVirtual.DelayedCall(2f, (ChangeState));
            LeanTween.delayedCall(this.gameObject, 2f, (ChangeState));
        }

    }

    public void SetDraggable(bool isDraggable)
    {
        bCanDrag = isDraggable;
    }
}
