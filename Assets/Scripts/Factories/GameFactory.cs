using ProductionGame.Controllers;
using ProductionGame.Models;
using ProductionGame.Repositories;
using ProductionGame.UI;
using UnityEngine;

namespace ProductionGame.Factories
{
    public interface IGameFactory
    {
        void Create();
        void CreateResourceBuilding(int index);
        void Clear();
    }

    public class GameFactory : IGameFactory
    {
        private readonly IBuildingFactory _buildingFactory;
        private readonly IBuildingsViewRepository _buildingsViewRepository;
        private readonly GameContext _gameContext;
        private readonly IProcessingBuildingController _processingBuildingController;
        private readonly IMarketController _marketController;
        private readonly IResourceBuildingController _resourceBuildingController;


        public GameFactory(GameContext gameContext,
            IBuildingFactory buildingFactory,
            IResourceBuildingController resourceBuildingController,
            IBuildingsViewRepository buildingsViewRepository,
            IProcessingBuildingController processingBuildingController,
            IMarketController marketController)
        {
            _gameContext = gameContext;
            _buildingFactory = buildingFactory;
            _resourceBuildingController = resourceBuildingController;
            _buildingsViewRepository = buildingsViewRepository;
            _processingBuildingController = processingBuildingController;
            _marketController = marketController;
        }

        public void Create()
        {
            var resourceBuildingCount = _gameContext.ResourceBuildingCount;
            for (var i = 0; i < resourceBuildingCount; i++)
                CreateResourceBuilding(i);

            var processingBuilding = _buildingFactory.CreateProcessingBuilding();
            _buildingsViewRepository.Add(processingBuilding);
            processingBuilding.OnBuildingClicked += _processingBuildingController.ShowProcessingBuildingWindow;

            var market = _buildingFactory.CreateMarket();
            _buildingsViewRepository.Add(market);
            market.OnBuildingClicked += _marketController.ShowMarket;
        }

        public void CreateResourceBuilding(int index)
        {
            var resourceBuilding = _buildingFactory.CreateResourceBuilding(index);
            _buildingsViewRepository.Add(resourceBuilding);
            resourceBuilding.OnBuildingClicked += _resourceBuildingController.ShowResourceBuildingWindow;
        }

        public void Clear()
        {
            _buildingsViewRepository.Dispose();
            foreach (var buildingView in _buildingsViewRepository.GetAll())
                Object.Destroy(buildingView.gameObject);

            _buildingsViewRepository.Clear();
        }
    }
}