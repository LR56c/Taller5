using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class CharacterSideScroller : MonoBehaviour
{
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddMovementInput(float xAxisValue,float yAxisValue)
    {
        Vector2 moveDirection = new Vector2(xAxisValue, 0);

        GetComponent<Rigidbody2D>().position = GetComponent<Rigidbody2D>().position + moveDirection * moveSpeed * Time.deltaTime;

    }

    void ReadInput()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
        AddMovementInput(horizontalValue,verticalValue);


    }
    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }
}
