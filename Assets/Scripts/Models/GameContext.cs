using System;

namespace ProductionGame.Models
{
    public class GameContext
    {
        public int ResourceBuildingCount { get; private set; }

        public void SelectBuildingCount(int buildingCount)
        {
            if (buildingCount <= 0)
                throw new ArgumentOutOfRangeException();

            ResourceBuildingCount = buildingCount;
        }
    }
}