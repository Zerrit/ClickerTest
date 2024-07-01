using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ClickerTest.UI
{
    public class BaseScreen : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        public void Open()
        {
            _content.gameObject.SetActive(true);
        }

        public void Close()
        {
            _content.gameObject.SetActive(false);
        }
        
        public async virtual UniTask OpenAsync()
        {
            //TODO
        }

        public async virtual UniTask CloseASync()
        {
            //TODO
        }
    }
}