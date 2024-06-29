using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Clicker.MVP.Clicker.Model
{
    public class ClickerModel
    {
        public event Action<bool> OnDisplayingChanged;
        public event Action OnLevelUpped;

        public bool DisplayingStatus { get; set; }

        public int Points { get; private set; }
        public int ClicksCount { get; private set; }
        public int PointsPerClick { get; private set; }
        public int Level { get; private set; }
        public int LevelUpRequirement { get; private set; }
        public int ClicksBeforeLevelUp { get; private set; }

        public ClickerModel()
        {
            //TODO проверка сохранений

            Points = 0;
            ClicksCount = 0;
            PointsPerClick = 1;
            Level = 1;
            LevelUpRequirement = 10;
            ClicksBeforeLevelUp = LevelUpRequirement;
        }

        public void ChangeDisplayingStatus(bool status)
        {
            DisplayingStatus = status;

            OnDisplayingChanged?.Invoke(status);
        }

        public void ConfirmClick()
        {
            Debug.Log("КЛИК!");

            Points += PointsPerClick;
            ClicksCount++;
            ClicksBeforeLevelUp--;

            if (ClicksBeforeLevelUp == 0)
            {
                Level++;
                LevelUpRequirement *= 2;
                ClicksBeforeLevelUp = LevelUpRequirement;
                
                OnLevelUpped?.Invoke();
            }
        }

        /// <summary>
        /// Увеличивает количество очков за клик на указанное значение
        /// </summary>
        public void UpgradePointsPerClick(int value)
        {
            PointsPerClick += value;
        }
    }
}
