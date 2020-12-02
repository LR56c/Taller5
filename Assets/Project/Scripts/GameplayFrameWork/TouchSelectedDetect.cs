using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSelectedDetect : MonoBehaviour
{
     
    public void OnFingerDown(LeanFinger finger)
    {
        Vector2 touchWorldPosition = finger.GetWorldPosition(10);
        print(touchWorldPosition);

       Collider2D[] touchedObjects=  Physics2D.OverlapCircleAll(touchWorldPosition, .1f);
        for (int i = 0; i < touchedObjects.Length; i++)
        {
            if(touchedObjects[i].GetInstanceID() == GetComponent<Collider2D>().GetInstanceID() )
            {
                GetComponent<LeanSelectable>().IsSelected = true;
                return;
            }
        }
        GetComponent<LeanSelectable>().IsSelected = false;
    }

    public void OnFingerUp(LeanFinger finger)
    {
        GetComponent<LeanSelectable>().IsSelected = false;
    }

}
