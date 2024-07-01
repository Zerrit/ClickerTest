using ClickerTest.Data;
using ClickerTest.MVP.Header.Model;
using ClickerTest.Tools.Reactivity;
using ClickerTest.UI;
using UnityEngine;

namespace ClickerTest.MVP.Clicker.Model
{
    public class ClickerModel : IScreenModel
    {
        public int ScreenId => 2;
        
        public SimpleReativeProperty<bool> DisplayingStatus { get; set; }

        public float ProgressIndex => 1 - (float)ClicksBeforeLevelUp.Value / LevelUpRequirement.Value;

        public SimpleReativeProperty<int> ClicksCount { get; }
        public SimpleReativeProperty<int> PointsPerClick { get; }
        public SimpleReativeProperty<int> Level { get; }
        public SimpleReativeProperty<int> LevelUpRequirement { get;  }
        public SimpleReativeProperty<int> ClicksBeforeLevelUp { get; }

        private readonly HeaderModel _resourcesModel;
        private readonly IPersistentDataService _dataService;
        
        public ClickerModel(HeaderModel headerModel, IPersistentDataService dataService)
        {
            _resourcesModel = headerModel;
            _dataService = dataService;

            var data = _dataService.Progress;
            
            DisplayingStatus = new SimpleReativeProperty<bool>(false);

            ClicksCount = new SimpleReativeProperty<int>(data.ClickCount);
            PointsPerClick = new SimpleReativeProperty<int>(data.PointsPerClick);
            Level = new SimpleReativeProperty<int>(data.Level);
            LevelUpRequirement = new SimpleReativeProperty<int>(data.LevelUpRequirement);
            ClicksBeforeLevelUp = new SimpleReativeProperty<int>(data.ClicksBeforeLevelUp);

            _dataService.OnNeedSaving += SaveData;
        }

        public void ConfirmClick()
        {
            _resourcesModel.Points.Value += PointsPerClick.Value;

            ClicksCount.Value++;
            ClicksBeforeLevelUp.Value -= PointsPerClick.Value;

            if (ClicksBeforeLevelUp.Value <= 0)
            {
                Level.Value++;
                LevelUpRequirement.Value *= 2;

                var overPoints = Mathf.Abs(ClicksBeforeLevelUp.Value);
                ClicksBeforeLevelUp.Value = LevelUpRequirement.Value;
                ClicksBeforeLevelUp.Value -= overPoints;
            }
        }

        /// <summary>
        /// Увеличивает количество очков за клик на указанное значение
        /// </summary>
        public void UpgradePointsPerClick(int value)
        {
            PointsPerClick.Value += value;
        }

        public bool TryUpgradePointPerClick(int price, int bonus)
        {
            if (_resourcesModel.TrySpendPoints(price))
            {
                PointsPerClick.Value += bonus;
                return true;
            }

            return false;
        }

        public void SaveData()
        {
            _dataService.Progress.Level = Level.Value;
            _dataService.Progress.ClickCount = ClicksCount.Value;
            _dataService.Progress.PointsPerClick = PointsPerClick.Value;
            _dataService.Progress.LevelUpRequirement = LevelUpRequirement.Value;
            _dataService.Progress.ClicksBeforeLevelUp = ClicksBeforeLevelUp.Value;
        }
    }
}
