using System;
using ProductionGame.Factories;
using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.Controllers
{
    public class GamePlayController : IDisposable
    {
        private readonly GameContext _gameContext;
        private readonly IBuildingFactory _buildingFactory;

        private readonly Vector3[] _buildingPositions = { new(0, 0, 0), new(2, 0, 0), new(-2, 0, 0) };
        private readonly Vector3 _processingBuildingPosition = new(0, 0, 2);
        private readonly Vector3 _marketPosition = new(0, 0, -2);

        public GamePlayController(GameContext gameContext, IBuildingFactory buildingFactory)
        {
            _gameContext = gameContext;
            _buildingFactory = buildingFactory;
        }


        public void SetUp()
        {
            // Создание и расположение ресурсных построек
            int resourceBuildingCount = _gameContext.ResourceBuildingCount;
            for (var i = 0; i < resourceBuildingCount; i++)
            {
                GameObject resourceBuilding =
                    Instantiate(_resourceBuildingPrefab, _buildingPositions[i], Quaternion.identity);
                // Дополнительная логика для настройки ресурсных построек
            }

            // Создание и расположение перерабатывающей постройки
            GameObject processingBuilding =
                Instantiate(_processingBuildingPrefab, _processingBuildingPosition, Quaternion.identity);
            // Дополнительная логика для настройки перерабатывающей постройки

            // Создание и расположение рынка
            GameObject market = Instantiate(_marketPrefab, _marketPosition, Quaternion.identity);
            // Дополнительная логика для настройки рынка

            // Другая логика инициализации объектов на карте

            // Запуск игровой логики
            StartGame();
        }

        private void StartGame()
        {
            // Здесь можно добавить начальную игровую логику, если требуется
        }
    }
}
