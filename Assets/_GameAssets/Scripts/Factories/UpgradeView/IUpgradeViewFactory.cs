using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ClickerTest.Factories.UpgradeView
{
    public interface IUpgradeViewFactory
    {
        public UniTask<MVP.Shop.View.UpgradeView> Create(Transform parent, CancellationToken token);
    }
}