using System;
using ClickerTest.Configs;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.ModelLogic;
using ClickerTest.Tools.Reactivity;

namespace ClickerTest.MVP.Shop.Model
{
    public class ShopModel : IScreenModel
    {
        public int ScreenId => 1;

        public event Action<int> OnUpgradePurchaised;
        
        public SimpleReativeProperty<bool> DisplayingStatus { get; set; }

        public UpgradeConfig[] Upgrades { get; private set; }

        private readonly ClickerModel _clickerModel;
        private readonly UpgradeListConfig _config;

        public ShopModel(ClickerModel clickerModel, UpgradeListConfig config)
        {
            _clickerModel = clickerModel;
            _config = config;
            
            DisplayingStatus = new SimpleReativeProperty<bool>(false);
            Upgrades = _config.Upgrades;
        }

        public bool TryBuyUpgrade(int id)
        {
            if (_clickerModel.TrySpendPoints(Upgrades[id].Price))
            {
                OnUpgradePurchaised?.Invoke(id);
                return true;
            }

            return false;
        }
    }
}
