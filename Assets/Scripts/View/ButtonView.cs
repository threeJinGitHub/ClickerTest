using Clicker.Managers;
using Clicker.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Clicker.View
{
    public abstract class ButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonTextBox;

        [Inject] protected readonly BalanceManager BalanceManager;

        protected TextMeshProUGUI ButtonTextBox => buttonTextBox;
        protected Button Button => button;
        protected BusinessModel BusinessModel;

        public void Init(BusinessModel businessModel)
        {
            BusinessModel = businessModel;
            button.onClick.AddListener(() =>
            {
                BalanceManager.OnSuccessfulPurchase += SuccessfulPurchase;
                BalanceManager.OnFailedPurchase += Unsubscribe;
                Purchase();
            });
        
            BalanceManager.OnSuccessfulPurchase += BalanceChanged;
            SetButtonInfo();
        }
    
        protected void Unsubscribe()
        {
            BalanceManager.OnFailedPurchase -= Unsubscribe;
            BalanceManager.OnSuccessfulPurchase -= SuccessfulPurchase;
        }

        protected abstract void SetButtonInfo();

        protected abstract void Purchase();

        protected abstract void SuccessfulPurchase();

        protected abstract void BalanceChanged();
    }

    public enum UpgradeType
    {
        FirstUpgrade,
        SecondUpgrade
    }
}
