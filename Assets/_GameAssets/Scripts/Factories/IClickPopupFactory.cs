using System.Threading;
using ClickerTest.MVP.Clicker.View;
using Cysharp.Threading.Tasks;

namespace ClickerTest.Factories
{
    public interface IClickPopupFactory
    {
        public UniTask<ClickPopup> Create(CancellationToken token);
    }
}