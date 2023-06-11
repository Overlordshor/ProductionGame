using ProductionGame.Controllers;
using ProductionGame.Factories;
using ProductionGame.Models;
using ProductionGame.SO;
using ProductionGame.UI;
using UnityEngine;

namespace ProductionGame
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private BuildingSettings _buildingSettings;

        private MainMenuController _mainMenuController;
        [SerializeField] private MainMenuView _mainMenuView;

        private void Start()
        {
            var gameContext = new GameContext();
            var buildingFactory = new BuildingFactory(_buildingSettings);
            var gamePlayController = new GamePlayController(gameContext, buildingFactory);
            _mainMenuController = new MainMenuController(gameContext, _mainMenuView, gamePlayController);
            _mainMenuController.ShowMainMenuView();
        }

        private void OnDestroy()
        {
            _mainMenuController.Dispose();
        }
    }
}