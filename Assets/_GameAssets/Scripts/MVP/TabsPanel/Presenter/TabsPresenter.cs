using System;
using System.Threading;
using ClickerTest.MVP.TabsPanel.Model;
using ClickerTest.MVP.TabsPanel.View;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace ClickerTest.MVP.TabsPanel.Presenter
{
    public class TabsPresenter : IStartable, IDisposable
    {
        private readonly TabsModel _model;
        private readonly TabsPanelView _view;

        public TabsPresenter(TabsModel model, TabsPanelView view)
        {
            _model = model;
            _view = view;
        }

        public void Start()
        {
            for (int i = 0; i < _view.Tabs.Length; i++)
            {
                _view.Tabs[i].Initialize(i);

                _view.Tabs[i].OnClicked += _model.SwitchTab;
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _view.Tabs.Length; i++)
            {
                _view.Tabs[i].OnClicked -= _model.SwitchTab;
            }
        }


    }
}