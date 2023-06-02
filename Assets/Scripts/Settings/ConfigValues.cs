using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu]
    public class ConfigValues : ScriptableObject
    {
        public List<BusinessData> Businesses;
    
        [Serializable]
        public struct BusinessData
        {
            public int DelayIncome;
            public int BaseCost;
            public int BaseIncome;
            public List<Upgrade> Upgrades;
        }
    
        [Serializable]
        public struct Upgrade
        {
            public int Cost;
            public int Multiplier;
        }
    }
}