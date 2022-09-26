using System;
using System.Collections.Generic;
using System.Linq;
using Clicker.Models;
using UnityEngine;
using Zenject;

namespace Clicker.Managers
{
    public class IncomeDelayManager : MonoBehaviour
    {
        [Inject] private readonly BalanceManager _balanceManager;
        private List<BusinessModel> _businessModels;
    
        public void Init(List<BusinessModel> businessModels)
        {
            _businessModels = businessModels;
            foreach (var business in businessModels.Where(business => business.IsBought))
            {
                _balanceManager.MakePurchase(-(int) ((DateTime.Now - business.LastIncomeDate).TotalSeconds * business.Income /
                                                     business.Delay));
                business.ResetDate();
            }
        }
    
        private void Update()
        {
            foreach (var business in 
                     _businessModels.Where(business => business.IsBought && 
                                                       business.LastIncomeDate.AddSeconds(business.Delay) < DateTime.Now))
            {
                business.IncomeComplete();
            }
        }
    }
}


