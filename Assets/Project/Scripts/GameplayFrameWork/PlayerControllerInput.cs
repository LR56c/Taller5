using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerInput : MonoBehaviour
{
     

    void ReadInput()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
      
   
        GetComponent<CharacterTopDownMovement>().AddMovementInput(horizontalValue, verticalValue);


    }
    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }
}
