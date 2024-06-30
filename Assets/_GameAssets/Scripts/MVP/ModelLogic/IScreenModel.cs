using ClickerTest.Tools.Reactivity;

namespace ClickerTest.MVP.ModelLogic
{
    public interface IScreenModel
    {
        int ScreenId { get; }

        SimpleReativeProperty<bool> DisplayingStatus { get; set; }
    }
}