using System.Collections;
using System.Collections.Generic;
using Taller;
using UnityEngine;

public class AddScoreToGameManager : MonoBehaviour
{
    public int baseScore=1;
    public void AddScore( )
    {
        GameManager.Instance?.AddScore(baseScore);

    }
    public void AddScoreCustomValue(int ScoreToAdd)
    {
        GameManager.Instance?.AddScore(ScoreToAdd);

    }
    public void RestScore(int ScoreToAdd)
    {
        GameManager.Instance?.AddScore(-ScoreToAdd);
    }
}
