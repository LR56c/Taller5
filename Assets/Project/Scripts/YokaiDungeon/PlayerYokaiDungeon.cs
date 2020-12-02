using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYokaiDungeon : MonoBehaviour
{
    Vector2 swipeDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFingerUp(LeanFinger finger)
    {
          swipeDirection = finger.LastScreenPosition - finger.StartScreenPosition;

       // print(swipeDirection);
        if( Mathf.Abs(swipeDirection.x)>Mathf.Abs(swipeDirection.y))
        {
            if(swipeDirection.x<0)
            {
                swipeDirection.x = -1;
            }else
            {
                swipeDirection.x = 1;
            }
            swipeDirection.y = 0;

        }else
        {
            if (swipeDirection.y < 0)
            {
                swipeDirection.y = -1;
            }
            else
            {
                swipeDirection.y = 1;
            }
            swipeDirection.x = 0;
        } 

        if(CheckIfCanMoveMoveWithBlockStyle(swipeDirection)==true)
        {
            GetComponent<CharacterGridMovement>().MoveToDirectionWithVector(swipeDirection);

        }


    }

    bool CheckIfCanMoveYokaiDungeonStyle(Vector2 swipeDirection)
    {
        Vector2 targetDirection = (Vector2)transform.position + swipeDirection;

       Collider2D[] detectedTargets = Physics2D.OverlapCircleAll(targetDirection, .1f);

        for (int i = 0; i < detectedTargets.Length; i++)
        {
            if (detectedTargets[i].CompareTag("Block") == true)
            {
                detectedTargets[i].GetComponent<PushableBox>().PushMe(swipeDirection);
                return false;
            }
        }
        return true;
    }
    bool CheckIfCanMoveMoveWithBlockStyle(Vector2 swipeDirection)
    {
        Vector2 targetDirection = (Vector2)transform.position + swipeDirection;

        Collider2D[] detectedTargets = Physics2D.OverlapCircleAll(targetDirection, .1f);

        for (int i = 0; i < detectedTargets.Length; i++)
        {
            if (detectedTargets[i].CompareTag("Block") == true)
            {
                return detectedTargets[i].GetComponent<PushableBox>().PushMe(swipeDirection);
               
            }
        }
        return true;
    }

}
