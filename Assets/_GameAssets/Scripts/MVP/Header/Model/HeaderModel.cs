using ClickerTest.Tools.Reactivity;
using UnityEngine;

namespace ClickerTest.MVP.Header.Model
{
    public class HeaderModel
    {
        public SimpleReativeProperty<int> Points { get; }
        //TODO Другие типы ресурсов.

        public HeaderModel()
        {
            Points = new SimpleReativeProperty<int>(0);
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
    }
}