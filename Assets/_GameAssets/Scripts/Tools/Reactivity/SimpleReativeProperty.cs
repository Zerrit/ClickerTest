using System;

namespace ClickerTest.Tools.Reactivity
{
    public class SimpleReativeProperty<T> : ISimpleReativeProperty<T>
    {
        public event Action<T> OnChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }

        private T _value;

        public SimpleReativeProperty(T startValue)
        {
            _value = startValue;
        }
    }
}