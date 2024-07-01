using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ClickerTest.Factories.Upgrades
{
    public interface IUpgradeViewFactory
    {
        public UniTask<MVP.Shop.View.UpgradeView> Create(Transform parent, CancellationToken token);
    }
}