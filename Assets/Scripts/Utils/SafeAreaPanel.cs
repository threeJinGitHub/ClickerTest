using UnityEngine;
using UnityEngine.Serialization;

namespace Clicker.Utils
{
    public enum SafeAreaType
    {
        TopBottom,
        Top,
        Bottom,
    }

    public class SafeAreaPanel : MonoBehaviour
    {
        [SerializeField] private SafeAreaType type;

        private RectTransform _panel;

        private void Start()
        {
            _panel = GetComponent<RectTransform>();
            ApplySafeArea(Screen.safeArea);
        }

        private void ApplySafeArea(Rect r)
        {
            var newR = r;
            switch (type)
            {
                case SafeAreaType.Bottom:
                    newR.height = Screen.height - r.yMin;
                    newR.y = r.yMin;
                    break;
                case SafeAreaType.Top:
                    newR.height = Screen.height - (Screen.height - r.yMax);
                    newR.y = 0;
                    break;
                case SafeAreaType.TopBottom:
                    break;
            }
            var anchorMin = newR.position;
            var anchorMax = newR.position + newR.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }
    }
}
