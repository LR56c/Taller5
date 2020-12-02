using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DetectedPlayerEvent : UnityEvent<GameObject> { };

public class AIPerception : MonoBehaviour
{
    public float sightRadius = 1f;
    Collider2D[] collider2Ds;
    public DetectedPlayerEvent OnPlayerDetected; 

    public GameObject detectedPlayer;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// muestra gizmos en el editor solo cuando esta seleccionado
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,sightRadius);
    }

    /// <summary>
    /// siempre muestra gizmos en el editor
    /// </summary>
    void OnDrawGizmos()
    {
      //  Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    void CheckSightPerception()
    {
      collider2Ds=  Physics2D.OverlapCircleAll(transform.position, sightRadius);

        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if( collider2Ds[i].CompareTag("Player")==true)
            {
                detectedPlayer = collider2Ds[i].gameObject;

                OnPlayerDetected.Invoke(detectedPlayer);

                return;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckSightPerception();

    }
}
