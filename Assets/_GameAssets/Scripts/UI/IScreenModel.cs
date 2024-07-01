using ClickerTest.Tools.Reactivity;

namespace ClickerTest.UI
{
    public interface IScreenModel
    {
        int ScreenId { get; }

        SimpleReativeProperty<bool> DisplayingStatus { get; }
    }
}