
using UnityEngine;
using UnityEngine.UI;

namespace ClickerTest.MVP.TabsPanel.View
{
    public class TabsPanelView : MonoBehaviour
    {
        [field: SerializeField] public TabView[] Tabs { get; private set; }
    }
}