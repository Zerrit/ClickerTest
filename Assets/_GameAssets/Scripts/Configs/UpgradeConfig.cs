using UnityEngine;

namespace ClickerTest.Configs
{
    [CreateAssetMenu(menuName = "Upgrades/New Upgrade", fileName = "NewUpgrade")]
    public class UpgradeConfig : ScriptableObject
    {
        [field:SerializeField] public int Id { get; private set; }
        [field:SerializeField] public string Title { get; private set; }
        [field:SerializeField] public string Price { get; private set; }
        [field:SerializeField] public string Bonus { get; private set; }
    }
}
