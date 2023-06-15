using System;
using ProductionGame.Factories;
using ProductionGame.Infrastructure;
using ProductionGame.Models;
using ProductionGame.UI;
using UnityEngine.SceneManagement;

namespace ProductionGame.Controllers
{
    public class MainMenuController : IDisposable
    {
        private readonly GameContext _gameContext;
        private readonly IGameFactory _gameFactory;
        private readonly IGameDataSaver _gameDataSaver;
        private readonly IMainMenuView _mainMenuView;
        private readonly IVictoryWindowView _victoryWindowView;

        public MainMenuController(GameContext gameContext,
            IMainMenuView mainMenuView,
            IVictoryWindowView victoryWindowView,
            IGameFactory gameFactory,
            IGameDataSaver gameDataSaver)
        {
            _gameContext = gameContext;
            _mainMenuView = mainMenuView;
            _gameFactory = gameFactory;
            _gameDataSaver = gameDataSaver;
            _mainMenuView.OnStartGameClicked += StartGame;
            _mainMenuView.OnBuildCountSelected += SelectBuildCount;

            _victoryWindowView = victoryWindowView;
            _victoryWindowView.OnMainMenuClicked += Restart;

            _gameContext.OnGameWon += ShowVictoryWindow;

            SelectBuildCount(1);
        }

        public void Dispose()
        {
            _mainMenuView.OnBuildCountSelected -= SelectBuildCount;
            _mainMenuView.OnStartGameClicked -= StartGame;
            _victoryWindowView.OnMainMenuClicked -= Restart;
            _gameContext.OnGameWon -= ShowVictoryWindow;
        }

        public void ShowMainMenuView()
        {
            _mainMenuView.Show();
        }

        public void Restart()
        {
            _gameDataSaver.Reset();
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        private void ShowVictoryWindow()
        {
            _victoryWindowView.Show();
        }

        private void SelectBuildCount(int buildCount)
        {
            _gameFactory.Clear();
            _gameContext.SelectBuildingCount(buildCount);
            for (var i = 0; i < buildCount; i++)
                _gameFactory.CreateResourceBuilding(i);
        }

        private void StartGame()
        {
            _gameFactory.Create();
        }
    }
}