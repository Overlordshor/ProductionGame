using ProductionGame.GameView;
using ProductionGame.Models;
using ProductionGame.SO;
using UnityEngine;

namespace ProductionGame.Factories
{
    public interface IBuildingFactory
    {
        IBuildingView<ResourceBuildingModel> CreateResourceBuilding(int index);
        GameObject CreateProcessingBuilding();
        GameObject CreateMarket();
    }

    public class BuildingFactory : ViewFactory, IBuildingFactory
    {
        private readonly BuildingSettings _buildingSettings;

        public BuildingFactory(BuildingSettings buildingSettings)
        {
            _buildingSettings = buildingSettings;
        }

        public IBuildingView<ResourceBuildingModel> CreateResourceBuilding(int index)
        {
            var resourceBuildingSettings = _buildingSettings.ResourceBuildingSettings;
            var position = resourceBuildingSettings.ResourceBuildingPositions[index];
            var view = InstantiatePrefab(resourceBuildingSettings.ResourceBuildingMenuPrefab, position);
            var resourceBuildingModel = new ResourceBuildingModel(resourceBuildingSettings.ProductionInterval);
            view.Initialize(resourceBuildingModel);
            return view;
        }

        public GameObject CreateProcessingBuilding()
        {
            return InstantiatePrefab(_buildingSettings.ProcessingBuildingPrefab,
                _buildingSettings.ProcessingBuildingPosition);
        }

        public GameObject CreateMarket()
        {
            return InstantiatePrefab(_buildingSettings.MarketPrefab, _buildingSettings.MarketPosition);
        }
    }
}