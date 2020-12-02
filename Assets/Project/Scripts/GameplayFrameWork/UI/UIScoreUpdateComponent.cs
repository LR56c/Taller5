using System.Collections;
using System.Collections.Generic;
using Taller;
using TMPro;
using UnityEngine;

public class UIScoreUpdateComponent : MonoBehaviour
{
    [Tooltip("Si no ha agregado manualmente la referencia a textmeshpro ui lo busca automaticamente")]
    public TextMeshProUGUI scoreUIText;

    private void Awake()
    {
        if(scoreUIText==null)
        {
            scoreUIText = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        GameManager.OnScoreChange += GameManager_OnScoreChange;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChange -= GameManager_OnScoreChange;

    }


    private void GameManager_OnScoreChange(int ScoreValue)
    {
        scoreUIText?.SetText(ScoreValue.ToString());

    }

}
