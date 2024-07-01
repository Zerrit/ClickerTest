using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerTest.UI
{
    public class PopupScreen : MonoBehaviour
    {
        [SerializeField] private Image _fade;
        [SerializeField] private Image _content;
        
        [SerializeField] private float _appearTime;
        [SerializeField] private float _disappearTime;

        public async UniTask ShowAsync(CancellationToken token)
        {
            _content.transform.localScale = new Vector2(0f, 0f);

            _fade.gameObject.SetActive(true);
            _content.gameObject.SetActive(true);
            
            await _content.transform.DOScale(Vector3.one, _appearTime)
                .SetEase(Ease.Linear)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);
        }

        public async UniTask HideAsync(CancellationToken token)
        {
            await _content.transform.DOScale(Vector3.zero, _disappearTime)
                .SetEase(Ease.Linear)
                .AwaitForComplete(TweenCancelBehaviour.CompleteAndCancelAwait, token);

            _fade.gameObject.SetActive(false);
            _content.gameObject.SetActive(false);
        }
    }
}