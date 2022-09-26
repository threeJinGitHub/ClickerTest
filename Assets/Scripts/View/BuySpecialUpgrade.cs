using TMPro;
using UnityEngine;

namespace Clicker.View
{
    public class BuySpecialUpgrade : ButtonView
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private TextMeshProUGUI incomeTextBox;
    
        protected override void SetButtonInfo()
        {
            switch (upgradeType)
            {
                case UpgradeType.FirstUpgrade:
                    if (BusinessModel.FirstUpgradeIsBought)
                         Bought();
                    else
                        ButtonTextBox.text = $"{BusinessModel.FirstUpgradeName}\n{BusinessModel.FirstUpgradeCost}";
                    break;
                case UpgradeType.SecondUpgrade:
                    if (BusinessModel.SecondUpgradeIsBought)
                        Bought();
                    else
                        ButtonTextBox.text = $"{BusinessModel.SecondUpgradeName}\n{BusinessModel.SecondUpgradeCost}";
                    break;
            }
        }
    
        protected override void Purchase()
        {
            switch (upgradeType)
            {
                case UpgradeType.FirstUpgrade:
                    BalanceManager.MakePurchase(BusinessModel.FirstUpgradeCost);
                    break;
                case UpgradeType.SecondUpgrade:
                    BalanceManager.MakePurchase(BusinessModel.SecondUpgradeCost);
                    break;
            }
        }

        private void Bought()
        {
            Button.interactable = false;
            ButtonTextBox.text = "Куплено";
        }

        protected override void SuccessfulPurchase()
        {
            Unsubscribe();
            BusinessModel.BuySpecialUpgrade(upgradeType);
            Bought();
            incomeTextBox.text = $"Доход {BusinessModel.Income}";
        }
    
        protected override void BalanceChanged()
        {
            switch (upgradeType)
            {
                case UpgradeType.FirstUpgrade:
                    if (!BusinessModel.FirstUpgradeIsBought)
                        Button.interactable = BalanceManager.Balance >= BusinessModel.FirstUpgradeCost;
                    break;
                case UpgradeType.SecondUpgrade:
                    if (!BusinessModel.SecondUpgradeIsBought)
                        Button.interactable = BalanceManager.Balance >= BusinessModel.SecondUpgradeCost;
                    break;
            }
        }
    }
}

