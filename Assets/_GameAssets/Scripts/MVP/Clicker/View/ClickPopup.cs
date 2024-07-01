using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ClickerTest.MVP.Clicker.View
{
    public class ClickPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Initialize(int value)
        {
            _text.text = $"+{value}";
            _text.alpha = 1;

            _text.DOFade(0f, 0.5f).SetDelay(0.5f);
            transform.DOMove(transform.position + new Vector3(0, 50, 0), 1).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}