using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathComponent : MonoBehaviour
{
    public float moveSpeed = 1;
    public PathType pathType = PathType.CatmullRom;
    public List<Vector2> waypointsPath;
    private Vector2[] finalPath;
    private void OnEnable()
    {
        FollowPath();
    }
    void InitializaPath()
    {
        Vector2 playerPosTmp = transform.position;
        finalPath = new Vector2[waypointsPath.Count];

        for (int i = 0; i < waypointsPath.Count; i++)
        {
            finalPath[i]= playerPosTmp + waypointsPath[i];
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 playerPosTmp = transform.position;

        for (int i = 1; i < waypointsPath.Count; i++)
        {

            Gizmos.DrawLine(playerPosTmp + waypointsPath[i - 1], playerPosTmp + waypointsPath[i]);
        }
    }
    public void FollowPath()
    {
        InitializaPath();

        GetComponent<Rigidbody2D>().DOPath(finalPath, 1/moveSpeed, pathType).OnComplete(PathFinish);
    }

    void PathFinish()
    {
        gameObject.SetActive(false);
    }

}
