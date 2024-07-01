using ClickerTest.Configs;
using ClickerTest.Factories;
using ClickerTest.Factories.ClickPopup;
using ClickerTest.Factories.UpgradeView;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Clicker.Presenter;
using ClickerTest.MVP.Clicker.View;
using ClickerTest.MVP.Header.Model;
using ClickerTest.MVP.Header.Presenter;
using ClickerTest.MVP.Header.View;
using ClickerTest.MVP.Shop.Model;
using ClickerTest.MVP.Shop.Presenter;
using ClickerTest.MVP.Shop.View;
using ClickerTest.MVP.TabsPanel.Model;
using ClickerTest.MVP.TabsPanel.Presenter;
using ClickerTest.MVP.TabsPanel.View;
using ClickerTest.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace ClickerTest.Architecture
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private UpgradeListConfig _upgradeListConfig;
        
        [SerializeField] private Transform _clickPopupContainer;
        [SerializeField] private AssetReference _clickPopupPrefab;
        [SerializeField] private AssetReference _upgradeViewPrefab;

        [SerializeField] private HeaderView _headerView;
        [SerializeField] private TabsPanelView _tabsView;
        [SerializeField] private ClickerView _clickerView;
        [SerializeField] private ShopView _shopView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigs(builder);
            RegisterFactories(builder);

            RegisterModels(builder);
            RegisterViews(builder);
            RegisterPresenters(builder);
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterComponent(_upgradeListConfig);
        }
        
        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<HeaderModel>(Lifetime.Singleton);
            builder.Register<IScreenModel, ClickerModel>(Lifetime.Scoped).AsSelf();
            builder.Register<IScreenModel, ShopModel>(Lifetime.Scoped).AsSelf();
            builder.Register<TabsModel>(Lifetime.Singleton);
        }

        private void RegisterViews(IContainerBuilder builder)
        {
            builder.RegisterInstance(_headerView);
            builder.RegisterInstance(_tabsView);
            builder.RegisterInstance(_clickerView);
            builder.RegisterInstance(_shopView);
        }

        private void RegisterPresenters(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<HeaderPresenter>();
            builder.RegisterEntryPoint<TabsPresenter>();
            builder.RegisterEntryPoint<ClickerPresenter>();
            builder.RegisterEntryPoint<ShopPresenter>();
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ClickPopupFactory>(Lifetime.Singleton).As<IClickPopupFactory>()
                .WithParameter(_clickPopupPrefab)
                .WithParameter(_clickPopupContainer);

            builder.Register<UpgradeViewFactory>(Lifetime.Singleton).As<IUpgradeViewFactory>()
                .WithParameter(_upgradeViewPrefab);
        }
    }
}