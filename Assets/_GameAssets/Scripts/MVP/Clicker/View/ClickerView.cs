using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Clicker.MVP.Clicker.View
{
    public class ClickerView : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;
        
        [SerializeField] private Slider _levelSlider;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }

        public void Open()
        {
            
        }

        public void Close()
        {
            
        }

        public void SpawnClickPopup(int value)
        {
            
        }
    }
}
