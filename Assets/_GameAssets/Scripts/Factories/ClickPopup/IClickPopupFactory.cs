using System.Threading;
using Cysharp.Threading.Tasks;

namespace ClickerTest.Factories.ClickPopup
{
    public interface IClickPopupFactory
    {
        public UniTask<MVP.Clicker.View.ClickPopup> Create(CancellationToken token);
    }
}