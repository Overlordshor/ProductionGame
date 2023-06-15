using System.Linq;
using ProductionGame.Controllers;
using ProductionGame.Factories;
using ProductionGame.Infrastructure;
using ProductionGame.Models;
using ProductionGame.Repositories;
using ProductionGame.SO;
using ProductionGame.UI;
using UnityEngine;

namespace ProductionGame
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private MarketView _marketView;
        [SerializeField] private ProcessingBuildingMenuView _processingBuildingMenuView;
        [SerializeField] private ResourceBuildingMenuView _resourceBuildingMenuView;
        [SerializeField] private StorageView _storageView;
        [SerializeField] private VictoryWindowView _victoryWindowView;

        private readonly Disposables _disposables = new();

        private void Start()
        {
            var gameDataSaver = new GameDataSaver();
            var gameData = gameDataSaver.Load();
            var storageModel = new StorageModel(gameData.StorageResources);
            var playerModel = new PlayerModel(gameData.CoinCount);

            var gameResources = _gameSettings.ResourcesInfo.ToDictionary(key => key.ResourceType);
            var marketController =
                new MarketController(_marketView, playerModel, gameResources, gameDataSaver);
            var processingBuildingController =
                new ProcessingBuildingController(_processingBuildingMenuView, storageModel, gameResources);
            var storageController =
                new StorageController(playerModel, storageModel, _storageView, gameDataSaver, gameResources);

            var buildingFactory = new BuildingFactory(_gameSettings, storageController, _disposables, storageModel);
            var buildingsViewRepository = new BuildingsViewRepository();
            var resourceBuildingController =
                new ResourceBuildingController(_resourceBuildingMenuView, gameResources);

            var gameContext = new GameContext(playerModel, _gameSettings);
            var gameFactory = new GameFactory(gameContext, buildingFactory, resourceBuildingController,
                buildingsViewRepository, processingBuildingController, marketController);

            var mainMenuController =
                new MainMenuController(gameContext, _mainMenuView, _victoryWindowView, gameFactory, gameDataSaver);

            mainMenuController.ShowMainMenuView();
            storageController.ApplyResource();

            _disposables.Add(mainMenuController);
            _disposables.Add(buildingsViewRepository);
            _disposables.Add(_mainMenuView);
            _disposables.Add(_resourceBuildingMenuView);
            _disposables.Add(_processingBuildingMenuView);
            _disposables.Add(_marketView);
            _disposables.Add(marketController);
            _disposables.Add(_victoryWindowView);
            _disposables.Add(gameContext);
            _disposables.Add(storageController);
            _disposables.Add(storageModel);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}