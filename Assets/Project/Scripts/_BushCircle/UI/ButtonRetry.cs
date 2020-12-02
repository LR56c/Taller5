using Taller;
using UnityEngine;
using UnityEngine.UI;

namespace _BushCircle.UI
{
    public class ButtonRetry : MonoBehaviour
    {
        public Button mButton;
        
        private void Awake()
        {
            mButton.GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            mButton.onClick.AddListener(Retry);
        }

        private void OnDisable()
        {
            mButton.onClick.RemoveListener(Retry);
        }

        private void Retry()
        {
            GameManager.Instance.RestartGame();
        }
    }
}