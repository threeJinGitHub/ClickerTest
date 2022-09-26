using System;
using System.Globalization;
using Clicker.Managers;
using Clicker.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Clicker.Models
{
    public class BusinessModel
    {
        private readonly BusinessInfo _businessInfo;
        private readonly BusinessModelData _modelData;
        private readonly BalanceManager _balanceManager;
        private readonly int _id;
        public Action OnIncomeComplete;

        public string BusinessName { get; }

        public string FirstUpgradeName { get; }

        public string SecondUpgradeName { get; }
        
        public BusinessModel(int id, BusinessInfo businessInfo, BalanceManager balanceManager, Names names, SaveCallback saveCallback)
        {
            FirstUpgradeName = names.firstUpgradeName;
            SecondUpgradeName = names.secondUpgradeName;
            BusinessName = names.businessName;
            _balanceManager = balanceManager;
            _id = id;
            _businessInfo = businessInfo;
            _modelData = RestoreData();
            saveCallback.OnSaveData += SaveData;
        }

        public bool IsBought => _modelData.isBought;

        public int Income => _modelData.income;

        public float Delay => _businessInfo.incomeDelay;

        public int Level => _modelData.upgradeLevel;

        public int Cost => _businessInfo.baseCost;

        public int UpgradeCost => _modelData.upgradeCost;

        public int FirstUpgradeCost => _businessInfo.firstUpgradeCost;
        
        public int SecondUpgradeCost => _businessInfo.secondUpgradeCost;

        public bool FirstUpgradeIsBought => _modelData.firstUpgradeIsBought;
        
        public bool SecondUpgradeIsBought => _modelData.secondUpgradeIsBought;

        public DateTime LastIncomeDate => _lastIncomeDate;

        private DateTime _lastIncomeDate;

        public void BuyBusiness()
        {
            OnIncomeComplete?.Invoke();
            _lastIncomeDate = DateTime.Now;
            _modelData.isBought = true;
        }

        public void UpgradeBusiness()
        {
            _modelData.upgradeLevel++;
            RecalculateIncome();
            _modelData.upgradeCost = (Level + 1) * _businessInfo.baseIncome;
        }

        private void RecalculateIncome()
        {
            _modelData.income = Level * _businessInfo.baseCost *
                (100 + (_modelData.firstUpgradeIsBought ? 1 : 0) * _businessInfo.firstIncomeMultiplier +
                 (_modelData.secondUpgradeIsBought ? 1 : 0) * _businessInfo.secondIncomeMultiplier) / 100;
        }

        private void SaveData()
        {
            _modelData.lastIncomeDate = _lastIncomeDate.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString($"{nameof(BusinessModelData)}{_id}", JsonUtility.ToJson(_modelData));
        }

        private BusinessModelData RestoreData()
        {
            BusinessModelData businessModelData;
            if (PlayerPrefs.HasKey($"{nameof(BusinessModelData)}{_id}"))
            {
                businessModelData =
                    JsonUtility.FromJson<BusinessModelData>(PlayerPrefs.GetString($"{nameof(BusinessModelData)}{_id}"));
            }
            else
            {
                businessModelData = new BusinessModelData
                {
                    firstUpgradeIsBought = false,
                    secondUpgradeIsBought = false,
                    isBought = _id == 0 ,
                    lastIncomeDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    upgradeLevel = 1,
                    income = _businessInfo.baseIncome,
                    upgradeCost = _businessInfo.baseCost,
                    
                };
            }
            
            _lastIncomeDate = DateTime.Parse(businessModelData.lastIncomeDate);
            
            return businessModelData;
        }

        public void BuySpecialUpgrade(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.FirstUpgrade:
                    _modelData.firstUpgradeIsBought = true;
                    RecalculateIncome();
                    break;
                case UpgradeType.SecondUpgrade:
                    _modelData.secondUpgradeIsBought = true;
                    RecalculateIncome();
                    break;
            }
        }

        public void ResetDate() => _lastIncomeDate = DateTime.Now;

        public void IncomeComplete()
        {
            _lastIncomeDate = DateTime.Now;
            _balanceManager.MakePurchase(-Income);
            OnIncomeComplete?.Invoke();
        }
        
        [Serializable]
        private class BusinessModelData
        {
            public string lastIncomeDate;
            public bool firstUpgradeIsBought;
            public bool secondUpgradeIsBought;
            public bool isBought;
            public int upgradeLevel;
            public int income;
            public int upgradeCost;
        }

    }
}

