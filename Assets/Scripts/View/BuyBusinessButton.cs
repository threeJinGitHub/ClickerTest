using UnityEngine;
using UnityEngine.Serialization;

namespace Clicker.View
{
    public class BuyBusinessButton : ButtonView
    {
        [SerializeField] private BusinessView businessView;

        protected override void SetButtonInfo()
        {
            ButtonTextBox.text = $"BUY\n{BusinessModel.Cost}";
            Button.interactable = BalanceManager.Balance >= BusinessModel.Cost;
        }

        protected override void Purchase()
        {
            if(!BusinessModel.IsBought)
                BalanceManager.MakePurchase(BusinessModel.Cost);
        }

        protected override void SuccessfulPurchase()
        {
            Unsubscribe();
            BusinessModel.BuyBusiness();
            Button.interactable = false;
            businessView.BuyingBusiness();
        }

        protected override void BalanceChanged()
        {
            if (!BusinessModel.IsBought)
            {
                Button.interactable = BalanceManager.Balance >= BusinessModel.Cost;
            }
        }
    }
}

