using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;
using DG.Tweening;

public enum EProjectileBehaviour { Normal,FollowPath}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectilecomponent : SerializedMonoBehaviour
{
    Rigidbody2D rb;

    public float projectileSpeed = 2f;
    public float projectileDamage = 1f;
    public string targetTag;
    public string targetMethod = "GetDamage";

    [BoxGroup("EProjectileBehaviour")]
    public EProjectileBehaviour defaulBehaviour = EProjectileBehaviour.Normal;

    [BoxGroup("EProjectileBehaviour/FollowPath")]
    public PathType pathType = PathType.CatmullRom;
    public Ease pathFollowEase = Ease.Linear;
    [FoldoutGroup("LoopType", expanded: false)]
    public LoopType defaultLoopType = LoopType.Restart;
    [FoldoutGroup("LoopType", expanded: false)]
    public int defaultLoopCount =0;
    [BoxGroup("EProjectileBehaviour/FollowPath/Waypoints")]
    public List<Vector2> waypointsPath;
    private Vector2[] finalPath;


    private void OnDrawGizmosSelected()
    {
        if(defaulBehaviour==EProjectileBehaviour.FollowPath)
        {
         Vector2 playerPosTmp = transform.position;

                for (int i = 1; i < waypointsPath.Count; i++)
                {

                    Gizmos.DrawLine(playerPosTmp + waypointsPath[i - 1], playerPosTmp + waypointsPath[i]);
                }
        }
       
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
   
    }
    private void OnDisable()
    {
        ResetTweens();

    }
    void ResetTweens()
    {
        DOTween.Kill(rb);
        DOTween.Kill(gameObject);
        DOTween.ClearCachedTweens();
    }
    public void LaunchProjectile( )
    {
        
        SetVelocity(transform.forward * projectileSpeed);
    }
    public void LaunchProjectile(Vector2 LaunchDirection)
    {

        GetComponent<Rigidbody2D>().velocity = LaunchDirection * projectileSpeed;
    }
    public void LaunchProjectile(Vector2 LaunchDirection, float LaunchSpeed = 2f)
    {
        projectileSpeed = LaunchSpeed;
        GetComponent<Rigidbody2D>().velocity = LaunchDirection * projectileSpeed;
    }
    public void LaunchProjectile(float LaunchSpeed=2f)
    {
        projectileSpeed = LaunchSpeed;
        SetVelocity(transform.forward * projectileSpeed);
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity*projectileSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.CompareTag(targetTag)==true)
        {
            ApplyDamageTotarget(collision.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag) == true)
        {
            ApplyDamageTotarget(collision.gameObject);
        }
    }

    void ApplyDamageTotarget(GameObject targetGameObject)
    {
        targetGameObject.SendMessage(targetMethod, projectileDamage,SendMessageOptions.DontRequireReceiver);

        gameObject.SetActive(false);
    }

  
    ///*Specif for Follow Path behaviour*///
    void InitializaPath()
    {
        ResetTweens();
        Vector2 playerPosTmp = transform.position;
        finalPath = new Vector2[waypointsPath.Count];

        for (int i = 0; i < waypointsPath.Count; i++)
        {
            finalPath[i] = playerPosTmp + waypointsPath[i];
        }
    }
    public void LaunchProjectileWithPath( )
    {
        LaunchProjectileWithPath(projectileSpeed);
    }
    public void LaunchProjectileWithPath(float LaunchSpeed = 2f)
    {
       
        projectileSpeed = LaunchSpeed;
        FollowPath();
    }
    public void FollowPath()
    {
        InitializaPath();
      float distance = Vector3.Distance(finalPath[0], finalPath[finalPath.Length-1]);
        rb.DOPath(finalPath, 1 / (projectileSpeed*0.1f)  , pathType).SetEase(pathFollowEase).OnComplete(PathFinish).SetLoops(defaultLoopCount, defaultLoopType);

     // rb.DOPath(finalPath, 1 / projectileSpeed, pathType) .OnComplete(PathFinish);
    }

    void PathFinish()
    {
        gameObject.SetActive(false);
    }

}
