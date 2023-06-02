using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu]
    public class ConfigNames : ScriptableObject
    {
        public List<string> Business;
        public List<string> Upgrade;
        public string Cost;
        public string Bought;
    }
}
