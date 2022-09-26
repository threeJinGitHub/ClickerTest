using System.Collections.Generic;
using Clicker.Models;
using Clicker.View;
using UnityEngine;
using Zenject;

namespace Clicker.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BusinessNameInfo businessName;
        [SerializeField] private List<BusinessInfo> businessInformation;
        [SerializeField] private List<BusinessView> businessesViews;
        [SerializeField] private IncomeDelayManager incomeDelayManager;
    
        [Inject] private readonly BalanceManager _balanceManager;
        [Inject] private readonly SaveCallback _saveCallback;

        private void Awake()
        {
            var models = new List<BusinessModel>(); 
            for (var i = 0; i < businessInformation.Count; i++)
            {
                var businessModel = new BusinessModel(i, businessInformation[i], _balanceManager, businessName.businessName[i], _saveCallback);
                models.Add(businessModel);
                businessesViews[i].Init(businessModel);
            }
            incomeDelayManager.Init(models);
        }
    }
}
