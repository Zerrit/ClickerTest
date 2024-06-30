
using UnityEngine;
using UnityEngine.UI;

namespace ClickerTest.MVP.TabsPanel.View
{
    public class TabsView : MonoBehaviour
    {
        [field:SerializeField] public Button ShopTab { get; private set; }
        [field:SerializeField] public Button ClickerTab { get; private set; }
    }
}