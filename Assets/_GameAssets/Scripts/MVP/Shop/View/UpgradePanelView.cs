using ClickerTest.ViewLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerTest.MVP.Shop.View
{
    public class UpgradePanelView : PopupScreen
    {
        [field:SerializeField] public Button CloseButton { get; private set; }
        [field:SerializeField] public TextMeshProUGUI Title { get; private set; }
        [field:SerializeField] public TextMeshProUGUI Price { get; private set; }
        [field:SerializeField] public TextMeshProUGUI Description { get; private set; }
        [field:SerializeField] public Button BuyButton { get; private set; }
    }
}