using System;
using ClickerTest.Configs;
using ClickerTest.MVP.Clicker.Model;

namespace ClickerTest.MVP.Shop.Model
{
    public class ShopModel
    {
        public bool IsInitialized { get; private set; }

        public event Action<bool> OnDisplayingChanged;

        public bool DisplayingStatus { get; private set; }
        
        public UpgradeConfig[] Upgrades { get; private set; }

        private readonly ClickerModel _clickerModel;
        private readonly UpgradeListConfig _config;

        public ShopModel(ClickerModel clickerModel, UpgradeListConfig config)
        {
            _clickerModel = clickerModel;
            _config = config;

            Initialize();
        }

        private void Initialize()
        {
            Upgrades = _config.Upgrades;
        }

        public void ChangeDisplayingStatus(bool status)
        {
            DisplayingStatus = status;

            OnDisplayingChanged?.Invoke(status);
        }

        public bool TryBuyUpgrade(int id)
        {
            return false;
        }
    }
}
