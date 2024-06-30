using System.Collections.Generic;
using System.Threading;
using ClickerTest.Factories.UpgradeView;
using ClickerTest.MVP.Shop.Model;
using ClickerTest.MVP.Shop.View;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace ClickerTest.MVP.Shop.Presenter
{
    public class ShopPresenter : IAsyncStartable
    {
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

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            Initialize();
        }

        private void Initialize()
        {
            _upgradeViews = new();
            _cts = new CancellationTokenSource();

            _model.DisplayingStatus.OnChanged += UpdateDisplaying;
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

            foreach (var upgrade in _model.Upgrades)
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

        private void ShowUpgradePopup(int id)
        {
            var upgradeData = _model.Upgrades[id];
            ShowUpgradePopupAsync(_cts.Token).Forget();
            
            async UniTask ShowUpgradePopupAsync(CancellationToken token)
            {
                _view.UpgradePopup.Title.text = upgradeData.Title;
                _view.UpgradePopup.Description.text = upgradeData.Bonus.ToString();
                _view.UpgradePopup.Price.text = upgradeData.Price.ToString();
                _view.UpgradePopup.BuyButton.onClick.AddListener(BuyUpgrade);
                _view.UpgradePopup.CloseButton.onClick.AddListener(HideUpgradePopup);
                
                await _view.UpgradePopup.ShowAsync(token);
            }
        }

        private void HideUpgradePopup()
        {
            _view.UpgradePopup.HideAsync(_cts.Token).Forget();
        }

        private void BuyUpgrade()
        {
            
        }
    }
}
