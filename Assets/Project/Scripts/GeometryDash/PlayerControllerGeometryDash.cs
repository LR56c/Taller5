using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGeometryDash : MonoBehaviour
{
    public float moveSpeed = 1;
    public float jumpForce = 1;
    public float flyForce = 1;

    public bool isInFloor;
    public bool isFlyModeOn;

    public Sprite jumpSprite;
    public Sprite flySprite;

    private Vector2 targetVelocity;
    
    //Debug only
    public float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveForwardWithForce()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right*moveSpeed, ForceMode2D.Force); 
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Transformador") == true)
        {
            SwitchFlyMode();
        }
    }

    void SwitchFlyMode()
    {
        /*
            isFlyModeOn=true ---> isFlyModeOn=false  --->isFlyModeOn=true 
        
         */
        isFlyModeOn = !isFlyModeOn ;

        if(isFlyModeOn==true)
        {
            GetComponent<SpriteRenderer>().sprite = flySprite;
        }

        if (isFlyModeOn == false)
        {
            GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }

    }
    void MoveForwardConstant() 
    { 

        if( Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y)>0.1f)
        {
            isInFloor = false;
        }else
        {
            isInFloor = true;
        }

        targetVelocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    
        GetComponent<Rigidbody2D>().velocity = targetVelocity;

        /* 
        Vector2 targetVelocity = Vector2.right * moveSpeed + Vector2.up* GetComponent<Rigidbody2D>().velocity.y;
        si moveSpeed=6   y gravedad=8;
        Vector2 targetVelocity= new Vector2(6, 8);
        targetVelocity=(6,8);

        ///caso 2 por comp
       Vector2 targetVelocity = Vector2.right * moveSpeed + Vector2.up* GetComponent<Rigidbody2D>().velocity.y;
           Vector2 targetVelocity = (1,0) * 6 + (0,1)* 8;
        Vector2 targetVelocity = (6,0)   + (0,8) ;
       Vector2 targetVelocity = (6,8); 
        */



    }

    /// <summary>
    /// jump entra solamente si  isFlyModeOn es falso
    /// </summary>
    public void Jump()
    {
        if(isFlyModeOn==false)
        {
            //arriba=up = Vector(0,1,0) = Vector2.Up       
            if(isInFloor==true)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            
        }     
      
    }
    /// <summary>
    /// FlyUP entra solamente si  isFlyModeOn es true
    /// </summary>
    public void FlyUp()
    {
        if(isFlyModeOn==true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * flyForce, ForceMode2D.Force);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //MoveForwardWithForce(); sacar comentario si quieren usar fisica
        MoveForwardConstant();

        ///Ejemplo
        currentVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;
 

    }
}
