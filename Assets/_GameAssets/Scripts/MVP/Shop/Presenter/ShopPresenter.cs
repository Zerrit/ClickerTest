using System;
using System.Collections.Generic;
using System.Threading;
using ClickerTest.Factories.Upgrades;
using ClickerTest.MVP.Shop.Model;
using ClickerTest.MVP.Shop.View;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ClickerTest.MVP.Shop.Presenter
{
    public class ShopPresenter : IStartable, IDisposable
    {
        private UpgradeView _selectedUpgradeView;

        private List<UpgradeView> _upgradeViews;
        private CancellationTokenSource _cts;

        private readonly ShopModel _model;
        private readonly ShopView _view;
        private readonly IUpgradeViewFactory _factory;

        public ShopPresenter(ShopModel model, ShopView view, IUpgradeViewFactory factory)
        {
            _model = model;
            _view = view;
            _factory = factory;
        }

        public void Start()
        {
            _upgradeViews = new();
            _cts = new CancellationTokenSource();

            _model.DisplayingStatus.OnChanged += UpdateDisplaying;
            _view.UpgradePopup.BuyButton.onClick.AddListener(BuyUpgrade);
            _view.UpgradePopup.CloseButton.onClick.AddListener(HideUpgradePopup);

            _model.IsInitialized = true;
        }

        private void UpdateDisplaying(bool newStatus)
        {
            if (newStatus)
            {
                OpenAsync(_cts.Token).Forget();
            }
            else
            {
                _view.Close();
            }
        }

        private async UniTask OpenAsync(CancellationToken token)
        {
            await UpdateUpgradeListAsync();

            _view.Open();
        }

        private async UniTask UpdateUpgradeListAsync()
        {
            CleareUpgradeViews();

            foreach (var upgrade in _model.AvailableUpgrades.Values)
            {
                var view = await _factory.Create(_view.ProductsParent, _cts.Token);
                _upgradeViews.Add(view);

                view.UpgradeId = upgrade.Id;
                view.Title.text = upgrade.Title;

                view.OnClicked += ShowUpgradePopup;
            }
        }

        private void CleareUpgradeViews()
        {
            foreach (var view in _upgradeViews)
            {
                Object.Destroy(view.gameObject);
            }

            _upgradeViews.Clear();
        }

        private void ShowUpgradePopup(UpgradeView view)
        {
            _selectedUpgradeView = view;
            var data = _model.AvailableUpgrades[_selectedUpgradeView.UpgradeId];
            ShowUpgradePopupAsync(_cts.Token).Forget();
            
            async UniTask ShowUpgradePopupAsync(CancellationToken token)
            {
                _view.UpgradePopup.Title.text = data.Title;
                _view.UpgradePopup.Description.text = $"Увеличьте доход за клик на {data.Bonus.ToString()}";
                _view.UpgradePopup.Price.text = data.Price.ToString();

                await _view.UpgradePopup.ShowAsync(token);
            }
        }

        private void HideUpgradePopup()
        {
            _view.UpgradePopup.HideAsync(_cts.Token).Forget();
        }

        private void BuyUpgrade()
        {
            if (_model.TryBuyUpgrade(_selectedUpgradeView.UpgradeId))
            {
                RemoveUpgrade(_selectedUpgradeView);
                HideUpgradePopup();
            }
        }

        private void RemoveUpgrade(UpgradeView upgrade)
        {
            _upgradeViews.Remove(upgrade);
            Object.Destroy(upgrade.gameObject);
        }

        public void Dispose()
        {
            _model.DisplayingStatus.OnChanged -= UpdateDisplaying;

            _cts?.Dispose();
        }
    }
}
