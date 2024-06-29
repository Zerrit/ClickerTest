using Clicker.MVP.Clicker.Model;
using Clicker.MVP.Clicker.View;
using Clicker.ObjectPooler;
using VContainer.Unity;

namespace Clicker.MVP.Clicker.Presenter
{
    public class ClickerPresenter : IStartable
    {
        private readonly ClickerModel _model;
        private readonly ClickerView _view;
        private ClickPopupPooler _pooler;

        public ClickerPresenter(ClickerModel model, ClickerView view, ClickPopupPooler pooler)
        {
            _model = model;
            _view = view;
            _pooler = pooler;
        }

        public void Start()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            _model.OnDisplayingChanged += UpdateDisplaying;
            _view.OnClicked += ProcessClick;
        }

        private void UpdateDisplaying(bool newStatus)
        {
            if (newStatus)
            {
                _view.Open();
            }
            else
            {
                _view.Close();
            }
        }

        private void ProcessClick()
        {
            _view.SpawnClickPopup(_model.PointsPerClick);
            _model.ConfirmClick();
        }
    }
}
