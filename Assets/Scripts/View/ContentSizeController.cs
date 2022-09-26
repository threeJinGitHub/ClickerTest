using UnityEngine;

namespace Clicker.View
{
    public class ContentSizeController : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private BusinessView lastBusinessFrame;

        private void Update()
        {
            const float shift = 25f;
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                -lastBusinessFrame.LowestPoint + shift * 2);
        }
    }
}

