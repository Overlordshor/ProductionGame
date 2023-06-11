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
        IBuildingView<ProcessingBuildingModel> CreateProcessingBuilding();
        GameObject CreateMarket();
    }

    public class BuildingFactory : ViewFactory, IBuildingFactory
    {
        private readonly List<IDisposable> _disposables;
        private readonly GameSettings _gameSettings;
        private readonly IStorageController _storageController;

        public BuildingFactory(GameSettings gameSettings,
            IStorageController storageController,
            List<IDisposable> disposables)
        {
            _gameSettings = gameSettings;
            _storageController = storageController;
            _disposables = disposables;
        }

        public IBuildingView<ResourceBuildingModel> CreateResourceBuilding(int index)
        {
            var resourceBuildingSettings = _gameSettings.ResourceBuildingSettings[index];
            var position = resourceBuildingSettings.BuildingPosition;
            var view = InstantiateBuildingView<ResourceBuildingModel>(resourceBuildingSettings.BuildingPrefab,
                position);
            var resourceBuildingModel = new ResourceBuildingModel(resourceBuildingSettings.ProductionInterval);
            resourceBuildingModel.OnResourceProduced += _storageController.Add;
            resourceBuildingModel.OnDisposed += UnsubscribeAll;

            _disposables.Add(resourceBuildingModel);
            _disposables.Add(view);

            view.Initialize(resourceBuildingModel);
            return view;

            void UnsubscribeAll()
            {
                resourceBuildingModel.OnResourceProduced -= _storageController.Add;
                resourceBuildingModel.OnDisposed -= UnsubscribeAll;
            }
        }

        public IBuildingView<ProcessingBuildingModel> CreateProcessingBuilding()
        {
            var processingBuildingSettings = _gameSettings.ProcessingBuildingSettings;
            var position = processingBuildingSettings.BuildingPosition;
            var view = InstantiateBuildingView<ProcessingBuildingModel>(processingBuildingSettings.BuildingPrefab,
                position);
            var processingBuildingModel = new ProcessingBuildingModel(processingBuildingSettings.ProductionInterval);
            processingBuildingModel.OnProductProduced += _storageController.Add;
            processingBuildingModel.OnDisposed += UnsubscribeAll;

            _disposables.Add(processingBuildingModel);
            _disposables.Add(view);

            view.Initialize(processingBuildingModel);
            return view;

            void UnsubscribeAll()
            {
                processingBuildingModel.OnProductProduced -= _storageController.Add;
                processingBuildingModel.OnDisposed -= UnsubscribeAll;
            }
        }

        public GameObject CreateMarket()
        {
            return null;
            //return InstantiateBuildingView(_gameSettings.MarketPrefab, _gameSettings.MarketPosition);
        }
    }
}