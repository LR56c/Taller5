using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthAttribute : MonoBehaviour
{

    public float baseValue=1;
    public bool isDead = false;
    private float currentValue;

    public UnityEvent OnDeadEvent;


    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        currentValue = baseValue;
        isDead = false;
    }
    
    public void GetDamage(float DamageTaken)
    {
        if(isDead==true)
        {
            return;
        }


        currentValue -= DamageTaken;

        if(currentValue<=0)
        {
            SetDead();
        }
    }

    public void SetDead()
    {
        isDead = true;
        OnDeadEvent.Invoke();
    }


}
