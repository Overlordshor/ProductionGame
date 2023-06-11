using ProductionGame.GameView;
using ProductionGame.SO;
using UnityEngine;

namespace ProductionGame.Factories
{
    public interface IBuildingFactory
    {
        IResourceBuildingView CreateResourceBuilding(int index);
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

        public IResourceBuildingView CreateResourceBuilding(int index)
        {
            var position = _buildingPrefabs.ResourceBuildingPositions[index];
            return InstantiatePrefab(_buildingPrefabs.ResourceBuildingMenuPrefab, position);
        }

        public GameObject CreateProcessingBuilding()
        {
            return InstantiatePrefab(_buildingPrefabs.ProcessingBuildingPrefab,
                _buildingPrefabs.ProcessingBuildingPosition);
        }

        public GameObject CreateMarket()
        {
            return InstantiatePrefab(_buildingPrefabs.MarketPrefab, _buildingPrefabs.MarketPosition);
        }
    }
}