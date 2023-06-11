using System;
using System.Collections.Generic;
using ProductionGame.Controllers;
using ProductionGame.Factories;
using ProductionGame.Models;
using ProductionGame.Repositories;
using ProductionGame.SO;
using ProductionGame.UI;
using UnityEngine;

namespace ProductionGame
{
    public class StartUp : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();
        [SerializeField] private BuildingSettings _buildingSettings;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private ResourceBuildingMenuView _resourceBuildingMenuView;
        [SerializeField] private StorageView _storageView;

        private void Start()
        {
            var gameContext = new GameContext();
            var resourceBuildingController = new ResourceBuildingController(_resourceBuildingMenuView);
            var storageController = new StorageController(new StorageModel(), _storageView);
            var buildingFactory = new BuildingFactory(_buildingSettings, storageController, _disposables);
            var buildingsViewRepository = new BuildingsViewRepository();
            var gamePlayController = new GamePlayController(gameContext, buildingFactory, resourceBuildingController,
                buildingsViewRepository);
            var mainMenuController = new MainMenuController(gameContext, _mainMenuView, gamePlayController);

            mainMenuController.ShowMainMenuView();

            _disposables.Add(mainMenuController);
            _disposables.Add(buildingsViewRepository);
            _disposables.Add(_mainMenuView);
            _disposables.Add(_resourceBuildingMenuView);
        }

        private void OnDestroy()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
            _disposables.Clear();
        }
    }
}