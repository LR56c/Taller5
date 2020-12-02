using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTopDownMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    
    public void AddMovementInput(float xAxisValue, float yAxisValue)
    {
        Vector2 moveDirection = new Vector2(xAxisValue, yAxisValue);

        GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position + moveDirection * moveSpeed * Time.deltaTime;

    }
   
}
