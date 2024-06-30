using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ClickerTest.Factories.UpgradeView
{
    public class UpgradeViewFactory : IUpgradeViewFactory, IDisposable
    {
        private readonly AssetReference _prefab;

        private AsyncOperationHandle<GameObject> _upgradeViewOperationHandle;

        public UpgradeViewFactory(AssetReference prefab)
        {
            _prefab = prefab;
        }

        public async UniTask<MVP.Shop.View.UpgradeView> Create(Transform parent, CancellationToken token)
        {
            _upgradeViewOperationHandle = Addressables.InstantiateAsync(_prefab, parent);
            var cell = await _upgradeViewOperationHandle.WithCancellation(token);

            return cell.GetComponent<MVP.Shop.View.UpgradeView>();
        }

        public void Dispose()
        {
            Addressables.Release(_upgradeViewOperationHandle);
        }
    }
}