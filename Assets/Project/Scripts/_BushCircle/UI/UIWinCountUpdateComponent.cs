using Taller;
using TMPro;
using UnityEngine;

public class UIWinCountUpdateComponent : MonoBehaviour
{
    [Tooltip("Si no ha agregado manualmente la referencia a textmeshpro ui lo busca automaticamente")]
    public TextMeshProUGUI scoreUIText;
    public TextMeshProUGUI scoreUITargetWinCount;

    private void OnEnable()
    {
        GameManager.OnTargetWinCountChange += GameManager_OnScoreChange;
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(EGameStates param1)
    {
        if(param1!= EGameStates.GAMEPLAY) return;
        scoreUITargetWinCount?.SetText(Taller.GameManager.Instance.TargetWinCount.ToString());
    }

    private void OnDisable()
    {
        GameManager.OnTargetWinCountChange -= GameManager_OnScoreChange;
    }


    private void GameManager_OnScoreChange(int ScoreValue)
    {
        scoreUIText?.SetText(ScoreValue.ToString());
    }
}