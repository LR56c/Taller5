using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableWithTime : MonoBehaviour
{
    public float autoDisableTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(autoDisableTime);

        gameObject.SetActive(false);

    }
}
