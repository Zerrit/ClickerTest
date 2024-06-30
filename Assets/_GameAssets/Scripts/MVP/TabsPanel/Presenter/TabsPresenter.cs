using System.Threading;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Shop.Model;
using ClickerTest.MVP.TabsPanel.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace ClickerTest.MVP.TabsPanel.Presenter
{
    public class TabsPresenter : IAsyncStartable
    {
        private TabsView _view;
        private ClickerModel _clickerModel;
        private ShopModel _shopModel;

        public TabsPresenter(TabsView view, ClickerModel clickerModel, ShopModel shopModel)
        {
            _view = view;
            _clickerModel = clickerModel;
            _shopModel = shopModel;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            //TODO Проверка на инициализацию всех окон, закрытие всех кроме основного.

            Initialize();
        }

        private void Initialize()
        {
            _view.ClickerTab.onClick.AddListener(OpenClickerWindow);
            _view.ShopTab.onClick.AddListener(OpenShopWindow);
        }

        private void OpenClickerWindow()
        { 
            Debug.Log("Клик");
            _shopModel.ChangeDisplayingStatus(false);
            _clickerModel.ChangeDisplayingStatus(true);
        }
        
        private void OpenShopWindow()
        {
            Debug.Log("Клик");
            _clickerModel.ChangeDisplayingStatus(false);
            _shopModel.ChangeDisplayingStatus(true);

        }
    }
}