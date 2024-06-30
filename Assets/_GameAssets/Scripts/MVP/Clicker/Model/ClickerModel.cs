using ClickerTest.MVP.ModelLogic;
using ClickerTest.Tools.Reactivity;

namespace ClickerTest.MVP.Clicker.Model
{
    public class ClickerModel : IScreenModel
    {
        public int ScreenId => 2;
        
        public SimpleReativeProperty<bool> DisplayingStatus { get; set; }

        public SimpleReativeProperty<int> Points { get; private set; }
        public SimpleReativeProperty<int> ClicksCount { get; private set; }
        public SimpleReativeProperty<int> PointsPerClick { get; private set; }
        public SimpleReativeProperty<int> Level { get; private set; }
        public SimpleReativeProperty<int> LevelUpRequirement { get; private set; }
        public SimpleReativeProperty<int> ClicksBeforeLevelUp { get; private set; }

        public float ProgressIndex => 1 - (float)ClicksBeforeLevelUp.Value / LevelUpRequirement.Value;

        public ClickerModel()
        {
            //TODO проверка сохранений
            DisplayingStatus = new SimpleReativeProperty<bool>(false);

            Points = new SimpleReativeProperty<int>(0);
            ClicksCount = new SimpleReativeProperty<int>(0);
            PointsPerClick = new SimpleReativeProperty<int>(1);
            Level = new SimpleReativeProperty<int>(1);
            LevelUpRequirement = new SimpleReativeProperty<int>(10);
            ClicksBeforeLevelUp = new SimpleReativeProperty<int>(LevelUpRequirement.Value);
        }

        public void ConfirmClick()
        {
            Points.Value += PointsPerClick.Value;
            ClicksCount.Value++;
            ClicksBeforeLevelUp.Value--;

            if (ClicksBeforeLevelUp.Value == 0)
            {
                Level.Value++;
                LevelUpRequirement.Value *= 2;
                ClicksBeforeLevelUp.Value = LevelUpRequirement.Value;
            }
        }

        /// <summary>
        /// Увеличивает количество очков за клик на указанное значение
        /// </summary>
        public void UpgradePointsPerClick(int value)
        {
            PointsPerClick.Value += value;
        }

        public bool TrySpendPoints(int amount)
        {
            if (Points.Value >= amount)
            {
                Points.Value -= amount;
                return true;
            }

            return false;
        }
    }
}
