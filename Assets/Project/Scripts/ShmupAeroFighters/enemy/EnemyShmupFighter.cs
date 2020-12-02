using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShmupFighter : MonoBehaviour
{
    public Vector3 startRotation=  new Vector3(0, 0, 180);
    private void OnEnable()
    {
        transform.localEulerAngles = startRotation;
    }
    // Start is called before the first frame update
    public void Destroyenemy()
    {
        gameObject.SetActive(false);
    }
}
