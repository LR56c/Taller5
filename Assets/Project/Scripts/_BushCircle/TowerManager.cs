using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerObject[] _towers;

    private void Awake()
    {
        _towers = GetComponentsInChildren<TowerObject>();
    }
}
