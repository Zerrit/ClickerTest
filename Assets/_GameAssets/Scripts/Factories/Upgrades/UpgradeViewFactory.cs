using System;
using System.Threading;
using ClickerTest.MVP.Shop.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ClickerTest.Factories.Upgrades
{
    public class UpgradeViewFactory : IUpgradeViewFactory, IDisposable
    {
        private readonly AssetReference _prefab;

        private AsyncOperationHandle<GameObject> _upgradeViewOperationHandle;

        public UpgradeViewFactory(AssetReference prefab)
        {
            _prefab = prefab;
        }

        public async UniTask<UpgradeView> Create(Transform parent, CancellationToken token)
        {
            _upgradeViewOperationHandle = Addressables.InstantiateAsync(_prefab, parent);
            var cell = await _upgradeViewOperationHandle.WithCancellation(token);

            return cell.GetComponent<UpgradeView>();
        }

        public void Dispose()
        {
            if (_upgradeViewOperationHandle.IsValid())
            {
                Addressables.Release(_upgradeViewOperationHandle);
            }
        }
    }
}