using System;
using System.Collections.Generic;

namespace ClickerTest.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int Points;
        public int Level;
        public int ClickCount;
        public int PointsPerClick;
        public int LevelUpRequirement;
        public int ClicksBeforeLevelUp;
        public List<int> PurchaisedUpgrades;
        
        public PlayerProgress(int points, int level, int clickCount, int pointsPerClick, int levelUpRequirement, int clicksBeforeLevelUp)
        {
            Points = points;
            Level = level;
            ClickCount = clickCount;
            PointsPerClick = pointsPerClick;
            LevelUpRequirement = levelUpRequirement;
            ClicksBeforeLevelUp = clicksBeforeLevelUp;

            PurchaisedUpgrades = new List<int>();
        }
    }
}