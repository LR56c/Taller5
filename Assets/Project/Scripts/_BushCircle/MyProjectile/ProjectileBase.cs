using System;
using System.Collections;
using System.Collections.Generic;
using _BushCircle;
using DG.Tweening;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Rigidbody2D _rb;
    public    float       Speed = 5f;

    public bool IsLaunched;
    public bool bLastest;

    public DraggableObjectController Parent;
    
    private Collider2D touchWall;
    private Vector3    targetRotation;
    
    public virtual void Launch()
    {
        if(IsLaunched) return;
        IsLaunched = true;

        _rb.velocity = Vector2.zero;
        _rb.velocity  = transform.up * Speed;
    }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if(bLastest) Parent.CheckStateGlobal();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(IsLaunched)
        {
            CheckIsTower(other);
        }
        else
        {
             CheckWallDirection(other.gameObject.GetComponent<DraggableRotationAreaDetect>());
        }
    }
    
    protected virtual void CheckIsTower(Collision2D other)
    {
        TowerObject tower = other.gameObject.GetComponent<TowerObject>();

        if(tower == null) return;
            
        TowerCollisionCallback(tower.TowerType,tower,other);
    }

    protected virtual void TowerCollisionCallback(ETowersType towerType, TowerObject towerObject,  Collision2D other)
    {
        
    }

    public void CheckWallDirection(DraggableRotationAreaDetect detector)
    {
        if(detector == null) return;
        if(IsLaunched) return;
        
            switch(detector.Directions)
            {
                case EDirections.UP:
                    targetRotation.z = 180f; break;     
                
                case EDirections.RIGHT:
                    targetRotation.z = 90f; break;     
                
                case EDirections.DOWN:
                    targetRotation.z = 0f; break;     
                
                case EDirections.LEFT:
                    targetRotation.z = 270f; break;     
                
                case EDirections.UP_LEFT:
                    targetRotation.z = 225f; break; 

                case EDirections.UP_RIGHT:
                    targetRotation.z = 135f; break;     

                case EDirections.DOWN_RIGHT:
                    targetRotation.z = 45f; break;     
                
                case EDirections.DOWN_LEFT: 
                    targetRotation.z = 315f; break;
                
                default: throw new ArgumentOutOfRangeException();
            }

            _rb.SetRotation(targetRotation.z);
    }

    public void DragToPosition(Vector3 touchWorldPosition)
    {
        if(IsLaunched) return;
        _rb.position = touchWorldPosition;
    }
}
