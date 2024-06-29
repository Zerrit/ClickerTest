using Clicker.MVP.Clicker.Model;
using Clicker.MVP.Clicker.Presenter;
using Clicker.MVP.Clicker.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Clicker.Architecture
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private Transform _clickPopupContainer;
        [SerializeField] private ClickPopup _clickPopupPrefab;
        
        [SerializeField] private ClickerView _clickerView;
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ClickerModel>(Lifetime.Singleton);
            builder.RegisterInstance(_clickerView);
            builder.RegisterEntryPoint<ClickerPresenter>(Lifetime.Singleton);
        }
    }
}