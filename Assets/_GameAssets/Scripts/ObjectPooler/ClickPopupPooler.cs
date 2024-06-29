using System.Collections.Generic;
using System.Threading;
using Clicker.MVP.Clicker.View;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Clicker.ObjectPooler
{
    public class ClickPopupPooler
    {
        protected bool _autoExpend;
        protected ClickPopup _prefab;
        protected List<ClickPopup> pool = new ();

        public ClickPopupPooler(ClickPopup prefab, bool isAutoExpend = true)
        {
            _prefab = prefab;
            _autoExpend = isAutoExpend;
        }

        public async UniTask CreatePoolAsync(int count, CancellationToken token)
        {
            for (int i = 0; i < count; i++)
            {
                await CreateObjectAsync(token);
            }
        }

        public async UniTask<ClickPopup> CreateObjectAsync(CancellationToken token, bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(_prefab);
            
            createdObject.gameObject.SetActive(isActiveByDefault);
            pool.Add(createdObject);
            
            return createdObject;
        }

        public bool HasFreeElement(out ClickPopup element)
        {
            foreach (var elem in pool)
            {
                if (!elem.gameObject.activeInHierarchy)
                {
                    element = elem;
                    return true;
                }
            }

            element = null;
            return false;
        }

        public async UniTask<ClickPopup> GetFreeElementAsync(CancellationToken token)
        {
            if (HasFreeElement(out var element))
            {
                element.gameObject.SetActive(true);
                return element;
            }

            if (_autoExpend)
                return await CreateObjectAsync(token, true);

            throw new System.Exception(message:$"В пуле нет свободных элементов {typeof(ClickPopup)}");
        }

        public void DeactivateAllElements()
        {
            foreach (var elem in pool)
            {
                elem.gameObject.SetActive(false);
            }
        }
    }
}