using System;
using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public class MainMenuController : IDisposable
    {
        private readonly GameContext _gameContext;
        private readonly IMainMenuView _mainMenuView;

        public MainMenuController(GameContext gameContext,
            IMainMenuView mainMenuView, GamePlayController gamePlayController)
        {
            _gameContext = gameContext;
            _mainMenuView = mainMenuView;
            _mainMenuView.OnStartGameClicked += StartGame;
            _mainMenuView.OnBuildCountSelected += SelectBuildCount;
        }


        public void ShowMainMenuView()
        {
            _mainMenuView.Show();
        }

        public void Dispose()
        {
            _mainMenuView.OnBuildCountSelected -= SelectBuildCount;
            _mainMenuView.OnStartGameClicked -= StartGame;
            _mainMenuView.Dispose();
        }

        private void SelectBuildCount(int buildCount)
        {
            _gameContext.SelectBuildingCount(buildCount);
        }

        private void StartGame()
        {
        }
    }
}