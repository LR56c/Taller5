using Taller;
using UnityEngine;
using UnityEngine.UI;

namespace _BushCircle.UI
{
    public class ButtonLevel : MonoBehaviour
    {
        public Button    mButton;
        public Transform parent;
        
        public void Inizialize()
        {
            mButton = GetComponent<Button>();
            parent = transform.parent;
            mButton.onClick.AddListener(GoLevel);
        }

        private void OnDisable()
        {
            mButton.onClick.RemoveListener(GoLevel);
        }

        private void GoLevel()
        {
            string parentNameLevel = parent.name;
            int level = int.Parse(parentNameLevel);
            GameManager.Instance.LoadLevel(level);
        }
    }
}