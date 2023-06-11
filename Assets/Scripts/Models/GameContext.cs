namespace ProductionGame.Models
{
    public class GameContext
    {
        public int BuildingCount { get; private set; }

        public void SelectBuildingCount(int buildingCount)
        {
            BuildingCount = buildingCount;
        }
    }
}