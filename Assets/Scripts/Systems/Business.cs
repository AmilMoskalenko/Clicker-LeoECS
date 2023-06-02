using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Systems
{
    public struct Business
    {
        public TextMeshProUGUI Name;
        public Slider Progress;
        public TextMeshProUGUI LevelText;
        public string LevelString;
        public int Level;
        public TextMeshProUGUI IncomeText;
        public string IncomeString;
        public float Income;
        public List<float> Multiplier;
        public Button LevelUpButton;
        public TextMeshProUGUI LevelUpText;
        public string LevelUpString;
        public int LevelUpCost;
        public List<Button> UpgradeButton;
        public List<TextMeshProUGUI> UpgradeText;
        public List<string> UpgradeString;
        public List<float> UpgradeCost;
    }
}
