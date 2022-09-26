using TMPro;
using UnityEngine;

namespace Clicker.View
{
    public class UpgradeLevel : ButtonView
    {
        [SerializeField] private TextMeshProUGUI _incomeTextBox;
        [SerializeField] private TextMeshProUGUI _levelTextBox;
    

        protected override void SetButtonInfo()
        {
            ButtonTextBox.text = $"LVL UP\n{BusinessModel.UpgradeCost}";
            _levelTextBox.text = $"LVL\n{BusinessModel.Level}";
            _incomeTextBox.text = $"Доход {BusinessModel.Income}";
        }

        protected override void Purchase()
        {
            BalanceManager.MakePurchase(BusinessModel.UpgradeCost);
        
        }

        protected override void SuccessfulPurchase()
        {
            Unsubscribe();
            BusinessModel.UpgradeBusiness();
            SetButtonInfo();
            Button.interactable = BalanceManager.Balance >= BusinessModel.UpgradeCost;
        }

        protected override void BalanceChanged()
        {
            Button.interactable = BalanceManager.Balance >= BusinessModel.UpgradeCost;
        }
    }
}

