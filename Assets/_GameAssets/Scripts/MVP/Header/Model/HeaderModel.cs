using ClickerTest.Data;
using ClickerTest.Tools.Reactivity;
using UnityEngine;

namespace ClickerTest.MVP.Header.Model
{
    public class HeaderModel
    {
        public SimpleReativeProperty<int> Points { get; }

        private readonly IPersistentDataService _dataService;

        public HeaderModel(IPersistentDataService dataService)
        {
            _dataService = dataService;

            Points = new SimpleReativeProperty<int>(_dataService.Progress.Points);

            _dataService.OnNeedSaving += SaveData;
        }

        public bool TrySpendPoints(int amount)
        {
            if (Points.Value >= amount)
            {
                Debug.Log(Points.Value);
                Debug.Log(amount);
                
                Points.Value -= amount;
                return true;
            }
            return false;
        }
        
        public void SaveData()
        {
            _dataService.Progress.Points = Points.Value;

        }
    }
}