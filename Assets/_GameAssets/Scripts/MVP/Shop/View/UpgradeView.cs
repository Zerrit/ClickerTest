using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClickerTest.MVP.Shop.View
{
    public class UpgradeView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<UpgradeView> OnClicked;
        
        public int UpgradeId { get; set; }
        
        [field:SerializeField] public TextMeshProUGUI Title { get; private set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}
