using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class MyTouchManager : MonoBehaviour
{
    private                              LeanSelect _leanSelect;

    private void Awake()
    {
        _leanSelect = GetComponent<LeanSelect>();
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += LeanTouchOnFingerDown;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= LeanTouchOnFingerDown;
    }

    private void LeanTouchOnFingerDown(LeanFinger obj)
    {
        _leanSelect.SelectScreenPosition(obj);
    }
}
