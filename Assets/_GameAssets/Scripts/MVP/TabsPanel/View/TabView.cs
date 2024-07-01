using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClickerTest.MVP.TabsPanel.View
{
    public class TabView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnClicked;
        
        public int OrderId { get; private set; }

        public void Initialize(int orderId)
        {
            OrderId = orderId;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(OrderId);
        }
    }
}