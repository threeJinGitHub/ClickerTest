using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Clicker.Managers
{
    public class BalanceManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceTextBox;
    
        public int Balance { get; private set; }

        public Action OnSuccessfulPurchase;

        public Action OnFailedPurchase;

        [Inject] private readonly SaveCallback _saveCallback;

        private void Awake()
        {
            RestoreData();
            balanceTextBox.text = $"Баланс: {Balance}$";
            _saveCallback.OnSaveData += SaveData;
        }

        public void MakePurchase(int cost)
        {
            if (Balance >= cost)
            {
                Balance -= cost;
                OnSuccessfulPurchase?.Invoke();
                balanceTextBox.text = $"{Balance}";
            }
            else
            {
                OnFailedPurchase?.Invoke();
            }
        }

        private void RestoreData()
        {
            Balance = PlayerPrefs.GetInt(nameof(BalanceManager), 0);
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(nameof(BalanceManager), Balance);
        }
    }    
}
