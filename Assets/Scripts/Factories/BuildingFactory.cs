using ProductionGame.SO;
using UnityEngine;

namespace ProductionGame.Factories
{
    public interface IBuildingFactory
    {
        GameObject CreateResourceBuilding();
        GameObject CreateProcessingBuilding();
        GameObject CreateMarket();
    }

    public class BuildingFactory : ViewFactory, IBuildingFactory
    {
        private readonly BuildingPrefabs _buildingPrefabs;

        public BuildingFactory(BuildingPrefabs buildingPrefabs)
        {
            _buildingPrefabs = buildingPrefabs;
        }

        public GameObject CreateResourceBuilding()
        {
            return InstantiatePrefab(_buildingPrefabs.ResourceBuildingPrefab);
        }

        public GameObject CreateProcessingBuilding()
        {
            return InstantiatePrefab(_buildingPrefabs.ProcessingBuildingPrefab);
        }

        public GameObject CreateMarket()
        {
            return InstantiatePrefab(_buildingPrefabs.MarketPrefab);
        }
    }
}