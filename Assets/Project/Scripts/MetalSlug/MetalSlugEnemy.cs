using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSlugEnemy : MonoBehaviour
{

    public GameObject targetPlayer;
    
  
    public void OnPlayerDetected(GameObject detectedPlayer)
    {
        targetPlayer = detectedPlayer;
    }

    void ShootPlayer()
    {

        if(targetPlayer != null)
        {
             print("shoot player");
        } 

    }
    public void GetDamage(float DamageAmmount)
    {
       
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        ShootPlayer();
    }
}
