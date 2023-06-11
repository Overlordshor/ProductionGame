using ProductionGame.GameView;
using ProductionGame.Models;
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
        private readonly BuildingSettings _buildingSettings;

        public BuildingFactory(BuildingSettings buildingSettings)
        {
            _buildingSettings = buildingSettings;
        }

        public IResourceBuildingView CreateResourceBuilding(int index)
        {
            var position = _buildingSettings.ResourceBuildingPositions[index];
            var view = InstantiatePrefab(_buildingSettings.ResourceBuildingMenuPrefab, position);

            var resourceBuildingModel = new ResourceBuildingModel();
            view.Initialize();
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