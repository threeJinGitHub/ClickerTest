using Clicker.Models;
using DG.Tweening;
using UnityEngine;

namespace Clicker.View
{
    public class IncomeBarView : MonoBehaviour
    {
        [SerializeField] private RectTransform progressBar;

        private RectTransform _fieldBar;
        private BusinessModel _businessModel;
        private Tween _incomeTween;

        private void Start()
        {
            _fieldBar = progressBar.transform.parent.GetComponent<RectTransform>();
            if (_businessModel.IsBought)
            {
                StartAnim();
            }
        }

        public void Init(BusinessModel businessModel)
        {
            _businessModel = businessModel;
            _businessModel.OnIncomeComplete += StartAnim;
        }

        private void StartAnim()
        {
            _incomeTween?.Kill();
            _incomeTween = 
                DOTween.To(() => 0f, x =>
                {
                    var sizeDelta = progressBar.sizeDelta;
                    sizeDelta = new Vector2(x * _fieldBar.sizeDelta.x, sizeDelta.y);
                    progressBar.sizeDelta = sizeDelta;
                    progressBar.anchoredPosition = new Vector2(sizeDelta.x / 2f, 0);
                }, 1f, _businessModel.Delay).SetEase(Ease.Linear);
        
        }
    }
}
