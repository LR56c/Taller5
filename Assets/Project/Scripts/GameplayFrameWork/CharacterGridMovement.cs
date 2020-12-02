using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterGridMovement : MonoBehaviour
{
    public float movingSpeed=1f; 
    public Ease easingMove=Ease.Linear;

    [Header("Overlap settings")]
    public Collider2D[] detectedTargets;
    public string BlockingTag; 
    
    public float circleCastRadius = .1f;
    public bool MoveToDirectionWithVector(Vector2 newDirection)
    {
            Vector2 targetDirection=(Vector2) transform.position+newDirection;

           detectedTargets=  Physics2D.OverlapCircleAll(targetDirection, circleCastRadius);

            for (int i = 0; i < detectedTargets.Length; i++)
            {
                if( detectedTargets[i].CompareTag(BlockingTag)==true)
                {
                    return false;
                }
            }

        float tweenTime =1/movingSpeed;
            GetComponent<Rigidbody2D>().DOMove(targetDirection,tweenTime).SetEase(easingMove);
        return true; 
    }
}
