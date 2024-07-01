using System;
using System.Collections.Generic;
using System.Linq;
using ClickerTest.Configs;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.Tools.Reactivity;
using ClickerTest.UI;

namespace ClickerTest.MVP.Shop.Model
{
    public class ShopModel : IScreenModel
    {
        public int ScreenId => 1;

        public SimpleReativeProperty<bool> DisplayingStatus { get; set; }

        public Dictionary<int, UpgradeConfig> AvailableUpgrades { get; }

        private readonly ClickerModel _clickerModel;

        public ShopModel(ClickerModel clickerModel, UpgradeListConfig config)
        {
            _clickerModel = clickerModel;

            DisplayingStatus = new SimpleReativeProperty<bool>(false);
            AvailableUpgrades = config.Upgrades.ToDictionary(x => x.Id, x => x);
        }

        public bool TryBuyUpgrade(int upgradeId)
        {
            if (!AvailableUpgrades.ContainsKey(upgradeId))
            {
                throw new Exception($"Попытка приобрести усиление с неопознанным Id:{upgradeId}");
            }
            
            if (_clickerModel.TryUpgradePointPerClick(AvailableUpgrades[upgradeId].Price, AvailableUpgrades[upgradeId].Bonus))
            {
                AvailableUpgrades.Remove(upgradeId);

                return true;
            }

            return false;
        }
    }
}
