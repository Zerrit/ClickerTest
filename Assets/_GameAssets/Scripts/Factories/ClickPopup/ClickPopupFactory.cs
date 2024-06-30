using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ClickerTest.Factories.ClickPopup
{
    public class ClickPopupFactory : IDisposable, IClickPopupFactory
    {
        private readonly AssetReference _prefab;
        private readonly Transform _parentContainer;

        private AsyncOperationHandle<GameObject> _popupOperationHandle;

        public ClickPopupFactory(AssetReference prefab, Transform parentContainer)
        {
            _prefab = prefab;
            _parentContainer = parentContainer;
        }

        public async UniTask<MVP.Clicker.View.ClickPopup> Create(CancellationToken token)
        {
            _popupOperationHandle = Addressables.InstantiateAsync(_prefab, _parentContainer);
            var cell = await _popupOperationHandle.WithCancellation(token);

            return cell.GetComponent<MVP.Clicker.View.ClickPopup>();
        }

        public void Dispose()
        {
            Addressables.Release(_popupOperationHandle);
        }
    }
}