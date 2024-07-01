using System.Threading;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Shop.Model;
using ClickerTest.MVP.TabsPanel.Model;
using ClickerTest.MVP.TabsPanel.View;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace ClickerTest.MVP.TabsPanel.Presenter
{
    public class TabsPresenter : IAsyncStartable
    {
        private readonly TabsModel _model;
        private readonly TabsPanelView _view;
        private readonly ClickerModel _clickerModel;
        private readonly ShopModel _shopModel;

        public TabsPresenter(TabsModel model, TabsPanelView view, ClickerModel clickerModel, ShopModel shopModel)
        {
            _model = model;
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
            for (int i = 0; i < _view.Tabs.Length; i++)
            {
                _view.Tabs[i].Initialize(i);

                _view.Tabs[i].OnClicked += _model.SwitchTab;
            }
            
            //_model.SwitchTab(2);
        }
    }
}