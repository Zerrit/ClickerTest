using System.Threading;
using ClickerTest.Factories;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Clicker.View;
using ClickerTest.ObjectPooler;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace ClickerTest.MVP.Clicker.Presenter
{
    public class ClickerPresenter : IAsyncStartable
    {
        private ClickPopupPooler _pooler;
        private CancellationTokenSource _cts;

        private readonly ClickerModel _model;
        private readonly ClickerView _view;
        private readonly IClickPopupFactory _factory;

        public ClickerPresenter(ClickerModel model, ClickerView view, IClickPopupFactory factory)
        {
            _model = model;
            _view = view;
            _factory = factory;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await InitializeAsync(cancellation);
        }

        private async UniTask InitializeAsync(CancellationToken token)
        {
            _cts = new CancellationTokenSource();

            _pooler = new ClickPopupPooler(_factory);
            await _pooler.CreatePoolAsync(20, token);

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
            SpawnClickPopupAsync(_model.PointsPerClick).Forget();
            _model.ConfirmClick();
        }
        
        public async UniTaskVoid SpawnClickPopupAsync(int value)
        {
            var popup = await _pooler.GetFreeElementAsync(_cts.Token);

            var randomX = Random.Range(0f, _view.PopupSpawnZoneSize.x);
            var randomY = Random.Range(0f, _view.PopupSpawnZoneSize.y);

            popup.transform.position = new Vector2(randomX, randomY);
            
            popup.Initialize(value);
        }
    }
}
