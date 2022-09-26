using UnityEngine;

namespace Clicker.Models
{
    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "ScriptableObjects/Business", order = 1)]
    public class BusinessInfo : ScriptableObject
    {
        public int incomeDelay;
        public int baseIncome;
        public int baseCost;
        public int firstIncomeMultiplier;
        public int secondIncomeMultiplier;
        public int firstUpgradeCost;
        public int secondUpgradeCost;
    }    
}


