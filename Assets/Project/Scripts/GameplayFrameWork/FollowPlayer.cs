using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
using UnityEngine.Events;

public class FollowPlayer : MonoBehaviour
{
    Transform playerTransform;
    public float moveSpeed = 1;
    public float moveForwardSpeed=1;
    public UnityEvent OnPathFinish;
    public float offSetRotationZ = 0;

    private void OnEnable()
    {
        if(playerTransform==null)
        {
            playerTransform= GameObject.FindGameObjectWithTag("Player").transform;

        }
        
        MoveToPlayer();
    }
   
    void MoveToPlayer()
    {
        MoveToPosition(playerTransform.position,PathFinish);

    }
    void MoveToPosition(Vector2 targetPosition, TweenCallback finishCallback=null)
    {
        if(finishCallback==null)
        {
            GetComponent<Rigidbody2D>().DOMove(targetPosition, 1 / moveSpeed) .OnUpdate(PathUpdate);

        }
        else
        {
            GetComponent<Rigidbody2D>().DOMove(targetPosition, 1 / moveSpeed).OnComplete(finishCallback).OnUpdate(PathUpdate);

        }
    }
    void PathUpdate()
    {
        Vector2 dirToPlayer =( playerTransform.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - offSetRotationZ);
       
    }
    void PathFinish()
    {
        OnPathFinish.Invoke();
        MoveForward();
    }
 
    void MoveForward()
    {

        GetComponent<Rigidbody2D>().velocity = transform.right * moveForwardSpeed;

    }

}
