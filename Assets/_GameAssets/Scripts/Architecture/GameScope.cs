using ClickerTest.Factories;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Clicker.Presenter;
using ClickerTest.MVP.Clicker.View;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace ClickerTest.Architecture
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Transform _clickPopupContainer;

        [SerializeField] private AssetReference _clickPopupPrefab;
        
        [SerializeField] private ClickerView _clickerView;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ClickPopupFactory>(Lifetime.Singleton).As<IClickPopupFactory>()
                .WithParameter(_clickPopupPrefab)
                .WithParameter(_clickPopupContainer);
            
            builder.Register<ClickerModel>(Lifetime.Singleton);
            builder.RegisterInstance(_clickerView);
            builder.RegisterEntryPoint<ClickerPresenter>();
        }
    }
}