using Leopotam.Ecs;
using Settings;
using TMPro;
using UnityEngine;

namespace Systems
{
    public class BalanceSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SavedKeys _savedKeys;
        private SpawnData _spawnData;

        public void Init()
        {
            var balanceEntity = _ecsWorld.NewEntity();
            ref var balance = ref balanceEntity.Get<Balance>();
            var balanceGO = Object.Instantiate(_spawnData.BalancePrefab, _spawnData.BalanceTransform);
            balance.BalanceText = balanceGO.GetComponent<TextMeshProUGUI>();
            balance.BalanceString = balance.BalanceText.text;
            balance.BalanceSum = PlayerPrefs.GetFloat(_savedKeys.BalanceKey, 0);
            balance.BalanceText.text = string.Format(balance.BalanceString, balance.BalanceSum);
        }
    }
}