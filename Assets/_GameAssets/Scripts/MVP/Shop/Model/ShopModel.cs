using System;
using System.Collections.Generic;
using ClickerTest.Configs;
using ClickerTest.Data;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.Tools.Reactivity;
using ClickerTest.UI;

namespace ClickerTest.MVP.Shop.Model
{
    public class ShopModel : IScreenModel
    {
        public bool IsInitialized { get; set; }
        public int ScreenId => 1;

        public SimpleReativeProperty<bool> DisplayingStatus { get; }

        public Dictionary<int, UpgradeConfig> AvailableUpgrades { get; }

        private readonly ClickerModel _clickerModel;
        private readonly IPersistentDataService _dataService;

        public ShopModel(ClickerModel clickerModel, UpgradeListConfig config, IPersistentDataService dataService)
        {
            _clickerModel = clickerModel;
            _dataService = dataService;

            DisplayingStatus = new SimpleReativeProperty<bool>(false);
            AvailableUpgrades = new Dictionary<int, UpgradeConfig>();

            foreach (var upgrade in config.Upgrades)
            {
                if (!_dataService.Progress.PurchaisedUpgrades.Contains(upgrade.Id))
                {
                    AvailableUpgrades[upgrade.Id] = upgrade;
                }
            }
        }

        public bool TryBuyUpgrade(int upgradeId)
        {
            if (!AvailableUpgrades.ContainsKey(upgradeId))
            {
                throw new Exception($"Попытка приобрести усиление с неопознанным Id:{upgradeId}");
            }

            if (_clickerModel.TryUpgradePointPerClick(AvailableUpgrades[upgradeId].Price, AvailableUpgrades[upgradeId].Bonus))
            {
                _dataService.Progress.PurchaisedUpgrades.Add(upgradeId);
                AvailableUpgrades.Remove(upgradeId);
                return true;
            }

            return false;
        }
    }
}
