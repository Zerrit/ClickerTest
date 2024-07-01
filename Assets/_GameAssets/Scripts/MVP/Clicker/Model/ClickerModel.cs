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

        //public SimpleReativeProperty<int> Points { get; private set; }
        public SimpleReativeProperty<int> ClicksCount { get; }
        public SimpleReativeProperty<int> PointsPerClick { get; }
        public SimpleReativeProperty<int> Level { get; }
        public SimpleReativeProperty<int> LevelUpRequirement { get;  }
        public SimpleReativeProperty<int> ClicksBeforeLevelUp { get; }

        private readonly HeaderModel _resourcesModel;
        
        public ClickerModel(HeaderModel headerModel)
        {
            _resourcesModel = headerModel;

            //TODO проверка сохранений
            DisplayingStatus = new SimpleReativeProperty<bool>(false);

            ClicksCount = new SimpleReativeProperty<int>(0);
            PointsPerClick = new SimpleReativeProperty<int>(1);
            Level = new SimpleReativeProperty<int>(1);
            LevelUpRequirement = new SimpleReativeProperty<int>(10);
            ClicksBeforeLevelUp = new SimpleReativeProperty<int>(LevelUpRequirement.Value);
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
    }
}
