using System;
using ProductionGame.SO;

namespace ProductionGame.Models
{
    public class GameContext : IDisposable
    {
        public event Action OnGameWon;

        private readonly PlayerModel _playerModel;
        private readonly GameSettings _gameSettings;

        public int ResourceBuildingCount { get; private set; }

        public GameContext(PlayerModel playerModel, GameSettings gameSettings)
        {
            _playerModel = playerModel;
            _gameSettings = gameSettings;
            _playerModel.OnCoinsChanged += HandleChangeCoins;
        }

        public void SelectBuildingCount(int buildingCount)
        {
            if (buildingCount <= 0)
                throw new ArgumentOutOfRangeException();

            ResourceBuildingCount = buildingCount;
        }

        private void HandleChangeCoins(int count)
        {
            if (count >= _gameSettings.CoinsToWin)
                OnGameWon?.Invoke();
        }


        public void Dispose()
        {
            _playerModel.OnCoinsChanged -= HandleChangeCoins;
            OnGameWon = null;
        }
    }
}