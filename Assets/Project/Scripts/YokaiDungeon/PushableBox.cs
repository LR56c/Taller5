using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushableBox : MonoBehaviour
{
    public bool PushMe(Vector2 pushDirection)
    {
      return  GetComponent<CharacterGridMovement>().MoveToDirectionWithVector(pushDirection);
    }
}
