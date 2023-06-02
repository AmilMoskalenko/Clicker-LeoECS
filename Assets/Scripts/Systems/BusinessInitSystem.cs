using System.Linq;
using Leopotam.Ecs;
using Settings;
using UnityEngine;

namespace Systems
{
    public class BusinessInitSystem : IEcsInitSystem
    {
        private ConfigNames _configNames;
        private ConfigValues _configValues;
        private SavedKeys _savedKeys;
        private EcsFilter<Business> _businessFilter;
        private EcsFilter<Balance> _balanceFilter;

        public void Init()
        {
            foreach (var idx in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(idx);
                var data = _configValues.Businesses[idx];
                business.Name.text = _configNames.Business[idx];
                business.LevelText.text = string.Format(business.LevelString, business.Level);
                business.Income = business.Level * data.BaseIncome;
                business.IncomeText.text = string.Format(business.IncomeString, business.Income);
                business.LevelUpCost = (business.Level + 1) * data.BaseCost;
                business.LevelUpText.text = string.Format(business.LevelUpString, business.LevelUpCost);
                for (int i = 0; i < business.UpgradeText.Count; i++)
                {
                    if (PlayerPrefs.GetInt(string.Format(_savedKeys.UpgradeKey, idx, i)) == 0)
                    {
                        var cost = string.Format(_configNames.Cost, business.UpgradeCost[i]);
                        business.UpgradeText[i].text = string.Format(business.UpgradeString[i],
                            _configNames.Upgrade[i], data.Upgrades[i].Multiplier, cost);
                    }
                    else
                    {
                        business.Multiplier[i] = data.Upgrades[i].Multiplier / 100f;
                        business.Income = business.Level * data.BaseIncome * (1 + business.Multiplier.Sum());
                        business.IncomeText.text = string.Format(business.IncomeString, business.Income);
                        business.UpgradeText[i].text = string.Format(business.UpgradeString[i],
                            _configNames.Upgrade[i], data.Upgrades[i].Multiplier, _configNames.Bought);
                        business.UpgradeButton[i].interactable = false;
                    }
                }
                business.LevelUpButton.onClick.AddListener(() =>
                {
                    ref var businessC = ref _businessFilter.Get1(idx);
                    ref var balance = ref _balanceFilter.Get1(0);
                    if (businessC.LevelUpCost <= balance.BalanceSum)
                    {
                        balance.BalanceSum -= businessC.LevelUpCost;
                        balance.BalanceText.text = string.Format(balance.BalanceString, balance.BalanceSum);
                        PlayerPrefs.SetFloat(_savedKeys.BalanceKey, balance.BalanceSum);
                        businessC.Level++;
                        businessC.LevelText.text = string.Format(businessC.LevelString, businessC.Level);
                        PlayerPrefs.SetInt(string.Format(_savedKeys.LevelKey, idx), businessC.Level);
                        businessC.Income = businessC.Level * data.BaseIncome * (1 + businessC.Multiplier.Sum());
                        businessC.IncomeText.text = string.Format(businessC.IncomeString, businessC.Income);
                        businessC.LevelUpCost = (businessC.Level + 1) * data.BaseCost;
                        businessC.LevelUpText.text = string.Format(businessC.LevelUpString, businessC.LevelUpCost);
                    }
                });
                for (int i = 0; i < business.UpgradeButton.Count; i++)
                {
                    var j = i;
                    business.UpgradeButton[i].onClick.AddListener(() =>
                    {
                        ref var businessC = ref _businessFilter.Get1(idx);
                        ref var balance = ref _balanceFilter.Get1(0);
                        if (businessC.UpgradeCost[j] <= balance.BalanceSum)
                        {
                            balance.BalanceSum -= businessC.UpgradeCost[j];
                            balance.BalanceText.text = string.Format(balance.BalanceString, balance.BalanceSum);
                            PlayerPrefs.SetFloat(_savedKeys.BalanceKey, balance.BalanceSum);
                            businessC.Multiplier[j] = data.Upgrades[j].Multiplier / 100f;
                            businessC.Income = businessC.Level * data.BaseIncome * (1 + businessC.Multiplier.Sum());
                            businessC.IncomeText.text = string.Format(businessC.IncomeString, businessC.Income);
                            businessC.UpgradeText[j].text = string.Format(businessC.UpgradeString[j],
                                _configNames.Upgrade[j], data.Upgrades[j].Multiplier, _configNames.Bought);
                            PlayerPrefs.SetInt(string.Format(_savedKeys.UpgradeKey, idx, j), 1);
                            businessC.UpgradeButton[j].interactable = false;
                        }
                    });
                }
            }
        }
    }
}
