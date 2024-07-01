using UnityEngine;

namespace ClickerTest.Configs
{
    [CreateAssetMenu(menuName = "Upgrades/Upgrades List", fileName = "UpgradesList")]
    public class UpgradeListConfig : ScriptableObject
    {
        [field:SerializeField] public UpgradeConfig[] Upgrades { get; private set; }
    }
}