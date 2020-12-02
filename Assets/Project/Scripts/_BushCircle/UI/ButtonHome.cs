using System;
using Taller;
using UnityEngine;
using UnityEngine.UI;

namespace _BushCircle.UI
{
    public class ButtonHome : MonoBehaviour
    {
        public Button mButton;
        
        private void Awake()
        {
            mButton.GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            mButton.onClick.AddListener(GoHome);
        }

        private void OnDisable()
        {
            mButton.onClick.RemoveListener(GoHome);
        }

        private void GoHome()
        {
            GameManager.Instance.LoadLevel(0);
        }
    }
}