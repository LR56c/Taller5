using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake                   Instance{get;private set;}
    private       CinemachineVirtualCamera           _cinemachineVirtualCamera;
    private       CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        /*DOTween.To(x => _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x, 0, intensity, time).OnComplete((() =>
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }));*/

        LeanTween.value(this.gameObject, x => _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = x,
                        0,intensity, time).setOnComplete((() =>
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }));
    }
}
