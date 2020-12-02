using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDragable : MonoBehaviour
{
    public float launchPower = 1;
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void OnBlockSwipe(LeanFinger finger)
    {


        if(finger.Down)
        {
            print("presiona inicio"+ finger.ScreenPosition);
        }
         
        if(finger.Up)
        {
            print("press release" + finger.ScreenPosition);
        }
    }

    public void OnTouchUp(LeanFinger finger)
    {
       // print( gameObject);
        Vector2 fingerWorldPosition = finger.GetStartWorldPosition(10);


        Collider2D collider = Physics2D.OverlapBox(fingerWorldPosition, new Vector2(0.1f, 0.1f), 0);

        if(collider ==null)
        {
            return;
        }

         

        if(GetComponent<Collider2D>().bounds.Contains(fingerWorldPosition)==false)
        {
            return;
        }


        Vector2 launchDirection = (finger.ScreenPosition-finger.StartScreenPosition);
        
        if( Mathf.Abs( launchDirection.x)>Mathf.Abs(launchDirection.y))//aca entra si horizontal es mas grande que vertical
        {
            if (launchDirection.x < 0)
            {
                launchDirection.x = -1;
            }                
            else
            {
                launchDirection.x = 1;
            }
            launchDirection.y = 0;

        }else//aca entra si vertical es mas grande que horizontal
        {
            if (launchDirection.y < 0)
            {
                launchDirection.y = -1;
            }
            else
            {
                launchDirection.y= 1;
            }
            launchDirection.x = 0;
        }

        //cambie impulse a velocity porque kinematic mas arcade
        GetComponent<Rigidbody2D>().velocity=launchDirection * launchPower;

      

    }
     
    void AdjustPosition()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;//lo detengo
        Vector2 adjustedPosition = transform.position;

        adjustedPosition.x = Mathf.Round(adjustedPosition.x ) ;
        adjustedPosition.y = Mathf.Round(adjustedPosition.y )  ;
        transform.position = adjustedPosition;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            AdjustPosition();
        }
    }
}