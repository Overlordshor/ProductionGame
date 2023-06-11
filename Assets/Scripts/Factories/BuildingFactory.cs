using System;
using System.Collections.Generic;
using ProductionGame.Controllers;
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
        private readonly IStorageController _storageController;
        private readonly List<IDisposable> _disposables;

        public BuildingFactory(BuildingSettings buildingSettings,
            IStorageController storageController,
            List<IDisposable> disposables)
        {
            _buildingSettings = buildingSettings;
            _storageController = storageController;
            _disposables = disposables;
        }

        public IBuildingView<ResourceBuildingModel> CreateResourceBuilding(int index)
        {
            var resourceBuildingSettings = _buildingSettings.ResourceBuildingSettings;
            var position = resourceBuildingSettings.ResourceBuildingPositions[index];
            var view = InstantiatePrefab(resourceBuildingSettings.ResourceBuildingMenuPrefab, position);
            var resourceBuildingModel = new ResourceBuildingModel(resourceBuildingSettings.ProductionInterval);
            resourceBuildingModel.OnResourceProduced += _storageController.AddResource;
            resourceBuildingModel.OnDisposed += UnsubscribeAll;

            _disposables.Add(resourceBuildingModel);
            _disposables.Add(view);

            view.Initialize(resourceBuildingModel);
            return view;

            void UnsubscribeAll()
            {
                resourceBuildingModel.OnResourceProduced -= _storageController.AddResource;
                resourceBuildingModel.OnDisposed -= UnsubscribeAll;
            }
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