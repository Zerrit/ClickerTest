using System.Threading;
using ClickerTest.Factories;
using ClickerTest.MVP.Clicker.Model;
using ClickerTest.MVP.Clicker.View;
using ClickerTest.ObjectPooler;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UIElements;
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
            
            _view.LevelSlider.value = 1 - (_model.ClicksBeforeLevelUp / _model.LevelUpRequirement);
            _view.LevelText.text = $"Level {_model.Level}";

            _model.OnDisplayingChanged += UpdateDisplaying;
            _model.OnClickCountChanged += UpdateClickCountText;
            _model.OnLevelChanged += UpdateLevelText;
            _model.OnClicksBeforeLevelUpChanged += UpdateProgressBar;
            _view.OnClicked += ProcessClick;
        }

        private void UpdateDisplaying(bool newStatus)
        {
            if (newStatus)
            {
                _view.LevelSlider.value = 1 - (_model.ClicksBeforeLevelUp / _model.LevelUpRequirement);
                _view.LevelText.text = $"Level {_model.Level}";
                
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

        private void UpdateClickCountText(int value)
        {
            _view.ClickCount.text = value.ToString();
        }
        
        private void UpdateLevelText(int level)
        {
            _view.LevelText.text = $"Level {_model.Level}";
        }
        
        private void UpdateProgressBar(int value)
        {
            _view.LevelSlider.value = 1f - (value / (float)_model.LevelUpRequirement);
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
