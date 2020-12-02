using System;
using System.Collections;
using System.Collections.Generic;
using _BushCircle.MyScriptableObject;
using DG.Tweening;
using Lean.Touch;
using MyNamespace.MyProjectile;
using Taller;
using TMPro;
using UnityEngine;

public class DraggableObjectController : MonoBehaviour
{
    private                  TextMeshPro     _textMeshPro;
    private                  LeanSelectable  _leanSelectable;
    private DraggableObjectManager _draggableObjectManager;
    
    [SerializeField] private GameObject _projectile;
    
    public ProjectileBase GameObjectHolding;
    public int            Count = 5;

    #region Initialize
    private void Awake()
    {
        _draggableObjectManager = GetComponentInParent<DraggableObjectManager>();
        _leanSelectable = GetComponent<LeanSelectable>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        UpdateTextCount();
    }

    private void OnEnable()
    {
        LeanSelectable.OnSelectUpGlobal += LeanSelectableOnSelectUpGlobal;
        LeanSelectable.OnSelectSetGlobal += LeanSelectableOnSelectSetGlobal;
        LeanSelectable.OnSelectGlobal += LeanSelectableOnSelectGlobal;
    }
    
    private void OnDisable()
    {
        LeanSelectable.OnSelectUpGlobal -= LeanSelectableOnSelectUpGlobal;
        LeanSelectable.OnSelectSetGlobal -= LeanSelectableOnSelectSetGlobal;
        LeanSelectable.OnSelectGlobal -= LeanSelectableOnSelectGlobal;
    }
    #endregion

    //start
    private void LeanSelectableOnSelectGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        if(_draggableObjectManager.bCanDrag) return;
        if(selectable.GetInstanceID() != _leanSelectable.GetInstanceID()) return;

        if(!CanCount()) return;

        Vector2 touchWorldPosition = finger.GetWorldPosition(10);
        GameObjectHolding = Instantiate(_projectile, touchWorldPosition, Quaternion.identity).GetComponent<ProjectileBase>();
        GameObjectHolding.Parent = this;
    }

    //update
    private void LeanSelectableOnSelectSetGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        if(GameObjectHolding == null) return;
        
        Vector2 touchWorldPosition = finger.GetWorldPosition(10);
        GameObjectHolding.DragToPosition(touchWorldPosition);

        if(_draggableObjectManager.bCanDrag)
        {
            Destroy(GameObjectHolding.gameObject);
            GameObjectHolding = null;
        }
    }
    
    //soltar
    private void LeanSelectableOnSelectUpGlobal(LeanSelectable selectable, LeanFinger finger)
    {
        if(GameObjectHolding == null) return;

        if(Count == 1) GameObjectHolding.bLastest = true;
        if(!CanDrag(finger)) return;
        
        GameObjectHolding.Launch();
        
        GameObjectHolding = null;
    }

    public void CheckStateGlobal()
    {
        if(Count <= 0) _draggableObjectManager.CheckState();
    }

    private bool CanDrag(LeanFinger finger)
    {
        Vector2 touchWorldPosition = finger.GetWorldPosition(10);
        
        Collider2D[] touchedObjects = Physics2D.OverlapCircleAll(touchWorldPosition, .1f);
      
        for(int i = 0; i < touchedObjects.Length; i++)
        {
            if(touchedObjects[i].GetComponent<DraggableObjectController>() != null ||
               touchedObjects[i].CompareTag("BlockZone"))
            {
                Destroy(GameObjectHolding.gameObject);
                GameObjectHolding = null;
                return false;
            }
        }
        
        CallFirstDrag();
        Count--;
        GameManager.Instance.DragCount++;
        UpdateTextCount();  
        
        return true;
    }

    private void CallFirstDrag()
    {
        if(GameManager.Instance.gameStates == EGameStates.MAIN_MENU)
        {
            GameManager.Instance.ChangeGameState(EGameStates.GAMEPLAY);
        }
    }

    private void UpdateTextCount()
    {
        _textMeshPro.SetText(Count.ToString());
    }
    
    private bool CanCount()
    {
        if(Count <= 0)
        {
            _leanSelectable.enabled = false;
            return false;
        }

        _leanSelectable.enabled = true;
        return true;
    }
}
