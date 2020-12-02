 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void FNotify ( );
public delegate void FNotify_1Params<T>(T Param1);

public delegate void FNotify_2Params<T1, T2>(T1 Param1, T2 Param2);
public delegate void FNotify_3Params<T1, T2,T3>(T1 Param1, T2 Param2, T3 Param3);

[System.Serializable] public class UnityEventOnTriggerEnter2D : UnityEvent<Collider2D> { }
[System.Serializable] public class UnityEventOnCollisionEnter2D : UnityEvent<Collision2D> { }

 