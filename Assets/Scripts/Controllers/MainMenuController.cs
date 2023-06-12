using System;
using ProductionGame.Factories;
using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public class MainMenuController : IDisposable
    {
        private readonly GameContext _gameContext;
        private readonly IGameFactory _gameFactory;
        private readonly IMainMenuView _mainMenuView;
        private readonly IVictoryWindowView _victoryWindowView;

        public MainMenuController(GameContext gameContext,
            IMainMenuView mainMenuView,
            IVictoryWindowView victoryWindowView,
            IGameFactory gameFactory)
        {
            _gameContext = gameContext;
            _mainMenuView = mainMenuView;
            _gameFactory = gameFactory;
            _mainMenuView.OnStartGameClicked += StartGame;
            _mainMenuView.OnBuildCountSelected += SelectBuildCount;

            _victoryWindowView = victoryWindowView;
            _victoryWindowView.OnMainMenuClicked += ShowMainMenuView;

            _gameContext.OnGameWon += ShowVictoryWindow;
        }


        public void ShowMainMenuView()
        {
            _mainMenuView.Show();
        }

        private void ShowVictoryWindow()
        {
            _victoryWindowView.Show();
        }

        private void SelectBuildCount(int buildCount)
        {
            _gameContext.SelectBuildingCount(buildCount);
        }

        private void StartGame()
        {
            _gameFactory.Create();
        }

        public void Dispose()
        {
            _mainMenuView.OnBuildCountSelected -= SelectBuildCount;
            _mainMenuView.OnStartGameClicked -= StartGame;
            _victoryWindowView.OnMainMenuClicked -= ShowMainMenuView;
            _gameContext.OnGameWon -= ShowVictoryWindow;
        }
    }
}