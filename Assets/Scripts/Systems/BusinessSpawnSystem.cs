using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Settings;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Systems
{
    public class BusinessSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private ConfigValues _configValues;
        private SavedKeys _savedKeys;
        private SpawnData _spawnData;

        public void Init()
        {
            for (int i = 0; i < _configValues.Businesses.Count; i++)
            {
                var businessEntity = _ecsWorld.NewEntity();
                ref var business = ref businessEntity.Get<Business>();
                business.Level = PlayerPrefs.GetInt(string.Format(_savedKeys.LevelKey, i), i == 0 ? 1 : 0);
                business.Multiplier = new List<float>();
                business.UpgradeButton = new List<Button>();
                business.UpgradeText = new List<TextMeshProUGUI>();
                business.UpgradeString = new List<string>();
                business.UpgradeCost = new List<float>();
                foreach (var upgrade in _configValues.Businesses[i].Upgrades)
                {
                    business.Multiplier.Add(0f);
                    business.UpgradeButton.Add(default);
                    business.UpgradeText.Add(default);
                    business.UpgradeString.Add(default);
                    business.UpgradeCost.Add(upgrade.Cost);
                }
                var businessGO = Object.Instantiate(_spawnData.BusinessPrefab, _spawnData.BusinessTransform);
                foreach (var text in businessGO.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    switch (text.GetComponent<TextController>().Type)
                    {
                        case TextType.Name:
                            business.Name = text;
                            break;
                        case TextType.Level:
                            business.LevelText = text;
                            business.LevelString = business.LevelText.text;
                            break;
                        case TextType.Income:
                            business.IncomeText = text;
                            business.IncomeString = business.IncomeText.text;
                            break;
                        case TextType.LevelUp:
                            business.LevelUpText = text;
                            business.LevelUpString = business.LevelUpText.text;
                            break;
                        case TextType.Upgrade1:
                            business.UpgradeText[0] = text;
                            business.UpgradeString[0] = business.UpgradeText[0].text;
                            break;
                        case TextType.Upgrade2:
                            business.UpgradeText[1] = text;
                            business.UpgradeString[1] = business.UpgradeText[1].text;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                business.Progress = businessGO.GetComponentInChildren<Slider>();
                foreach (var button in businessGO.GetComponentsInChildren<Button>())
                {
                    switch (button.GetComponent<ButtonController>().Type)
                    {
                        case ButtonType.LevelUp:
                            business.LevelUpButton = button;
                            break;
                        case ButtonType.Upgrade1:
                            business.UpgradeButton[0] = button;
                            break;
                        case ButtonType.Upgrade2:
                            business.UpgradeButton[1] = button;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}