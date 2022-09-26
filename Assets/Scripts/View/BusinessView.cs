using System.Collections.Generic;
using Clicker.Models;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Clicker.View
{
    public class BusinessView : MonoBehaviour
{
    [SerializeField] private RectTransform businessViewFrame;
    [SerializeField] private Transform purchasedViewHolder;
    [SerializeField] private Transform notPurchasedViewHolder;
    [SerializeField] private IncomeBarView incomeBarView;
    [SerializeField] private BusinessView previousView;
    [SerializeField] private BusinessView nextView;
    [SerializeField] private List<ButtonView> businessFrameButtons;
    [SerializeField] private TextMeshProUGUI businessNameTextBox;

    public float LowestPoint => businessViewFrame.anchoredPosition.y - businessViewFrame.sizeDelta.y / 2f;
    
    private Tween _moveTween;

    private void BringDown()
    {
        _moveTween?.Kill();
        var startPos = businessViewFrame.anchoredPosition;
        _moveTween = DOTween.To(() => 0,
            x => businessViewFrame.anchoredPosition =
                Vector2.Lerp(startPos, DestPos(), x), 1f, .35f);
        if (nextView != null)
        {
            nextView.BringDown();
        }
    }

    private Vector2 DestPos()
    {
        const int shift = 25;
        var destPos = new Vector2(0, -shift);
        if (previousView != null)
        {
            destPos.y += previousView.businessViewFrame.anchoredPosition.y -
                         previousView.businessViewFrame.sizeDelta.y / 2 - businessViewFrame.sizeDelta.y / 2f;
        }
        else
        {
            destPos.y -= businessViewFrame.sizeDelta.y / 2f;
        }
        return destPos;
    }

    public void BuyingBusiness()
    {
        const int height = 600;
        var frameSize = businessViewFrame.sizeDelta;
        businessViewFrame.DOSizeDelta( new Vector2(frameSize.x, height), .35f);
        
        var tweenSeq = DOTween.Sequence();
        tweenSeq.Append(notPurchasedViewHolder.DOScale(0, .25f));
        tweenSeq.AppendCallback(() =>
        {
            purchasedViewHolder.gameObject.SetActive(true);
            notPurchasedViewHolder.gameObject.SetActive(false);
        });
        
        for (var i = 0; i < purchasedViewHolder.childCount; i++)
        {
            purchasedViewHolder.GetChild(i).localScale = Vector3.zero;
            var j = i;
            tweenSeq.AppendCallback(() => purchasedViewHolder.GetChild(j).DOScale(1, .25f));
            tweenSeq.AppendInterval(.1f);
        }
        BringDown();
    }

    public void Init(BusinessModel businessModel)
    {
        businessFrameButtons.ForEach(button => button.Init(businessModel));
        incomeBarView.Init(businessModel);
        if (businessModel.IsBought)
        {
            BuyingBusiness();
        }
        businessNameTextBox.text = $"{businessModel.BusinessName}";
    }
}
}


