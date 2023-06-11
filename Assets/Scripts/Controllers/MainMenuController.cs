using System;
using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public class MainMenuController : IDisposable
    {
        private readonly GameContext _gameContext;
        private readonly IGamePlayController _gamePlayController;
        private readonly IMainMenuView _mainMenuView;

        public MainMenuController(GameContext gameContext,
            IMainMenuView mainMenuView,
            IGamePlayController gamePlayController)
        {
            _gameContext = gameContext;
            _mainMenuView = mainMenuView;
            _gamePlayController = gamePlayController;
            _mainMenuView.OnStartGameClicked += StartGame;
            _mainMenuView.OnBuildCountSelected += SelectBuildCount;
        }

        public void Dispose()
        {
            _mainMenuView.OnBuildCountSelected -= SelectBuildCount;
            _mainMenuView.OnStartGameClicked -= StartGame;
        }


        public void ShowMainMenuView()
        {
            _mainMenuView.Show();
        }

        private void SelectBuildCount(int buildCount)
        {
            _gameContext.SelectBuildingCount(buildCount);
        }

        private void StartGame()
        {
            _gamePlayController.CreateGame();
        }
    }
}