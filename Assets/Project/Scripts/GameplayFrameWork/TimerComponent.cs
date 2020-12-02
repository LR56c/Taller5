using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ETimerLoopType { Infinite,Finite}
public class TimerComponent : MonoBehaviour
{
    public bool autoBeginOnStart = true;
    public float waitTime = 1;
    public ETimerLoopType timerLoopType=ETimerLoopType.Infinite;
    /*Si loop count es 0 se entiende que es infinito*/
    public int loopCount = 0;
    private int currentLoop;

    public UnityEvent OnLoopFinish;

    private void OnEnable()
    {
        currentLoop = loopCount;
    }

    private void Start()
    {
        if(autoBeginOnStart==true)
        BeginTimer();
    }

    public void BeginTimer()
    {
        StartCoroutine(WaitCorutine());
    }

    IEnumerator WaitCorutine()
    {
        yield return new WaitForSeconds(waitTime);
        ExecuteTimerAction();

        if(timerLoopType==ETimerLoopType.Infinite)
        {
            BeginTimer();
        }
        else
        {
            currentLoop--;
            if (currentLoop > 0)
                BeginTimer();
        }
       


    }

    void ExecuteTimerAction()
    {
        OnLoopFinish.Invoke();

    }
}
