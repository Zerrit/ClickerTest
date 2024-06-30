using System;

namespace ClickerTest.Tools.Reactivity
{
    public interface ISimpleReativeProperty<T>
    {
        event Action<T> OnChanged;

        T Value { get; set; }
    }
}