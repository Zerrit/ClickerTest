using ClickerTest.UI;
using UnityEngine;

namespace ClickerTest.MVP.Shop.View
{
    public class ShopView : BaseScreen
    {
        [field:SerializeField] public Transform ProductsParent { get; private set; }
        [field:SerializeField] public UpgradePanelView UpgradePopup { get; private set; }
    }
}
