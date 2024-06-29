using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Clicker.MVP.Clicker.View
{
    public class ClickPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Initialize(int value)
        {
            _text.text = value.ToString();

            _text.DOFade(0f, 0.5f).SetDelay(0.5f);
            transform.DOMove(transform.position + new Vector3(0, 2, 0), 1).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}