using System.Collections.Generic;
using System.Threading;
using ClickerTest.Factories;
using ClickerTest.MVP.Clicker.View;
using Cysharp.Threading.Tasks;

namespace ClickerTest.ObjectPooler
{
    public class ClickPopupPooler
    {
        protected bool _autoExpend;
        protected IClickPopupFactory _factory;
        protected List<ClickPopup> pool = new ();

        public ClickPopupPooler(IClickPopupFactory factory, bool isAutoExpend = true)
        {
            _factory = factory;
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
            var createdObject = await _factory.Create(token);
            
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