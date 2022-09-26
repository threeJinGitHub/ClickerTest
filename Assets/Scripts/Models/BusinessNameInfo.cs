using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Models
{
    [CreateAssetMenu(fileName = "BusinessName", menuName = "ScriptableObjects/Names", order = 1)]
    public class BusinessNameInfo : ScriptableObject
    {
        public List<Names> businessName;
    }

    [Serializable]
    public class Names
    {
        public string businessName;
        public string firstUpgradeName;
        public string secondUpgradeName;
    }    
}
