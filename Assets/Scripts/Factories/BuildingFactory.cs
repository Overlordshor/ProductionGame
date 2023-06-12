using ProductionGame.Controllers;
using ProductionGame.GameView;
using ProductionGame.Infrasturcture;
using ProductionGame.Models;
using ProductionGame.SO;

namespace ProductionGame.Factories
{
    public interface IBuildingFactory
    {
        IBuildingView<ResourceBuildingModel> CreateResourceBuilding(int index);
        IBuildingView<ProcessingBuildingModel> CreateProcessingBuilding();
        IBuildingView<StorageModel> CreateMarket();
    }

    public class BuildingFactory : ViewFactory, IBuildingFactory
    {
        private readonly IDisposables _disposables;
        private readonly StorageModel _storageModel;
        private readonly GameSettings _gameSettings;
        private readonly IStorageController _storageController;

        public BuildingFactory(GameSettings gameSettings,
            IStorageController storageController,
            IDisposables disposables,
            StorageModel storageModel)
        {
            _gameSettings = gameSettings;
            _storageController = storageController;
            _disposables = disposables;
            _storageModel = storageModel;
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

        public IBuildingView<StorageModel> CreateMarket()
        {
            var marketBuildingSettings = _gameSettings.MarketBuildingSettings;
            var position = marketBuildingSettings.BuildingPosition;
            var view = InstantiateBuildingView<StorageModel>(marketBuildingSettings.BuildingPrefab, position);

            _disposables.Add(view);
            view.Initialize(_storageModel);
            return view;
        }
    }
}