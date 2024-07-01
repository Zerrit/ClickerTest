using System;
using ClickerTest.MVP.Header.Model;
using ClickerTest.MVP.Header.View;
using VContainer.Unity;

namespace ClickerTest.MVP.Header.Presenter
{
    public class HeaderPresenter : IStartable, IDisposable
    {
        private readonly HeaderModel _model;
        private readonly HeaderView _view;

        public HeaderPresenter(HeaderModel model, HeaderView view)
        {
            _model = model;
            _view = view;
        }

        public void Start()
        {
            UpdatePointsView(_model.Points.Value);

            _model.Points.OnChanged += UpdatePointsView;
        }

        private void UpdatePointsView(int value)
        {
            _view.PointsText.text = _model.Points.Value.ToString();
        }

        public void Dispose()
        {
            _model.Points.OnChanged -= UpdatePointsView;
        }
    }
}