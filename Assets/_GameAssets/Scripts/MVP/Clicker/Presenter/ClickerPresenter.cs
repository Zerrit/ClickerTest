using System;
using System.Threading;
using ClickerTest.Factories.ClickPopup;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Clicker.View;
using ClickerTest.ObjectPooler;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace ClickerTest.MVP.Clicker.Presenter
{
    public class ClickerPresenter : IAsyncStartable, IDisposable
    {
        private Vector2 _popupZoneMin;
        private Vector2 _popupZoneMax;

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
            _cts = new CancellationTokenSource();

            _pooler = new ClickPopupPooler(_factory);
            await _pooler.CreatePoolAsync(20, cancellation);
            GetSpawnCoords();

            _view.LevelSlider.value = _model.ProgressIndex;
            _view.LevelText.text = $"Level {_model.Level.Value}";
            _view.ClickCount.text = _model.ClicksCount.Value.ToString();

            _model.DisplayingStatus.OnChanged += UpdateDisplaying;
            _model.ClicksCount.OnChanged += UpdateClickCountText;
            _model.Level.OnChanged += UpdateLevelText;
            _model.ClicksBeforeLevelUp.OnChanged += UpdateProgressBar;

            _view.OnClicked += ProcessClick;

            _model.IsInitialized = true;
            _model.DisplayingStatus.Value = true;
        }

        private void UpdateDisplaying(bool newStatus)
        {
            if (newStatus)
            {
                _view.LevelSlider.value = _model.ProgressIndex;
                _view.LevelText.text = $"Level {_model.Level.Value}";
                
                _view.Open();
            }
            else
            {
                _view.Close();
            }
        }

        private void ProcessClick()
        {
            SpawnClickPopupAsync(_model.PointsPerClick.Value).Forget();
            _model.ConfirmClick();
        }

        private void UpdateClickCountText(int value)
        {
            _view.ClickCount.text = value.ToString();
        }

        private void UpdateLevelText(int level)
        {
            _view.LevelText.text = $"Level {_model.Level.Value}";
        }

        private void UpdateProgressBar(int value)
        {
            _view.LevelSlider.value = _model.ProgressIndex;
        }

        private async UniTaskVoid SpawnClickPopupAsync(int value)
        {
            var popup = await _pooler.GetFreeElementAsync(_cts.Token);

            var randomX = Random.Range(_popupZoneMin.x, _popupZoneMax.x);
            var randomY = Random.Range(_popupZoneMin.y, _popupZoneMax.y);

            popup.transform.position = new Vector2(randomX, randomY);
            
            popup.Initialize(value);
        }

        private void GetSpawnCoords()
        {
            var zone = _view.PopupSpawnZone;
            var rect = zone.rect;
            
            var xMin = zone.position.x - (rect.width / 2);
            var yMin = zone.position.y - (rect.height / 2);

            _popupZoneMin = new Vector2(xMin, yMin);
            _popupZoneMax = new Vector2(xMin + rect.width, yMin + rect.height);
        }

        public void Dispose()
        {
            _cts?.Dispose();
            
            _model.DisplayingStatus.OnChanged -= UpdateDisplaying;
            _model.ClicksCount.OnChanged -= UpdateClickCountText;
            _model.Level.OnChanged -= UpdateLevelText;
            _model.ClicksBeforeLevelUp.OnChanged -= UpdateProgressBar;

            _view.OnClicked -= ProcessClick;
        }
    }
}
