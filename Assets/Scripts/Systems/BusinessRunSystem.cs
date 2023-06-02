using Leopotam.Ecs;
using Settings;
using UnityEngine;

namespace Systems
{
    public class BusinessRunSystem : IEcsRunSystem
    {
        private ConfigValues _configValues;
        private SavedKeys _savedKeys;
        private EcsFilter<Business> _businessFilter;
        private EcsFilter<Balance> _balanceFilter;

        public void Run()
        {
            foreach (var idx in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(idx);
                if (business.Level > 0)
                {
                    business.Progress.value += Time.fixedDeltaTime / _configValues.Businesses[idx].DelayIncome;
                    if (business.Progress.value >= 1f)
                    {
                        business.Progress.value = 0f;
                        ref var balance = ref _balanceFilter.Get1(0);
                        balance.BalanceSum += business.Income;
                        balance.BalanceText.text = string.Format(balance.BalanceString, balance.BalanceSum);
                        PlayerPrefs.SetFloat(_savedKeys.BalanceKey, balance.BalanceSum);
                    }
                }
            }
        }
    }
}
