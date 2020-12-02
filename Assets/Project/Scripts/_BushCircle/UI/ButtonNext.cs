using System;
using Taller;
using UnityEngine;
using UnityEngine.UI;

namespace _BushCircle.UI
{
    public class ButtonNext : MonoBehaviour
    {
        public Button mButton;

        private void Awake()
        {
            mButton.GetComponent<Button>();
        }

        private void OnEnable()
        {
            mButton.onClick.AddListener(GoNext);
        }

        private void OnDisable()
        {
            mButton.onClick.RemoveListener(GoNext);
        }

        private void GoNext()
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}