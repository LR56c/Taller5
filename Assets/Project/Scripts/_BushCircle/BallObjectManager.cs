using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObjectManager : MonoBehaviour
{
    [SerializeField]             private BallObject[] _ballObjectControllers;

    private void Start()
    {
        Taller.GameManager.Instance.TargetWinCount = _ballObjectControllers.Length;
    }

    private void Awake()
    {
        _ballObjectControllers = GetComponentsInChildren<BallObject>();
    }
}
