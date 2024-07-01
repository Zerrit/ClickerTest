using UnityEngine;

namespace ClickerTest.Configs
{
    [CreateAssetMenu(menuName = "Progress/Start Progress", fileName = "StartProgress")]
    public class StartProgressConfig :ScriptableObject
    {
        [field:SerializeField] public int Points { get; private set; }
        [field:SerializeField] public int Level { get; private set; }
        [field:SerializeField] public int ClickCount { get; private set; }
        [field:SerializeField] public int PointsPerClick { get; private set; }
        [field:SerializeField] public int LevelUpRequirement { get; private set; }
    }
}