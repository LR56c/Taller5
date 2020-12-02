using System.Collections;
using System.Collections.Generic;
using Taller;
using UnityEngine;

public class AddToWinCount : MonoBehaviour
{
    public int defaultWinCount = 1;
    public void AddWinCount()
    {
        GameManager.Instance?.AddWinCount(defaultWinCount);

    }
    public void AddCustomWinCountValue(int CountValue)
    {
        GameManager.Instance?.AddWinCount(CountValue);

    }
    public void RestWinCount(int CountValue)
    {
        GameManager.Instance?.AddWinCount(-CountValue);
    }
}
