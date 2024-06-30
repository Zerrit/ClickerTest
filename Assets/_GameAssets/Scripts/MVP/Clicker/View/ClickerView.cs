using System;
using ClickerTest.ViewLogic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ClickerTest.MVP.Clicker.View
{
    public class ClickerView : BaseScreen, IPointerClickHandler
    {
        public event Action OnClicked;

        public Vector2 PopupSpawnZoneSize => PopupSpawnZone.sizeDelta;

        [field:SerializeField] public Slider LevelSlider { get; private set; }
        [field:SerializeField] public TextMeshProUGUI LevelText { get; private set; }
        [field:SerializeField] public TextMeshProUGUI ClickCount { get; private set; }
        [field:SerializeField] public RectTransform PopupSpawnZone { get; private set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}
