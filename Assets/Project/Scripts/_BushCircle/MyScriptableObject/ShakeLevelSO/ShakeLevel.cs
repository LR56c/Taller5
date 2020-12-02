using UnityEngine;

namespace _BushCircle.MyScriptableObject
{
    [CreateAssetMenu(fileName = "NewShakeLevel", menuName = "ScriptableObject/ShakeLevel", order = 0)]
    public class ShakeLevel : ScriptableObject
    {
        public float Intensity;
        public float Duration;
    }
}