using ProductionGame.Factories;
using ProductionGame.Models;
using ProductionGame.Repositories;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IGamePlayController
    {
        void CreateGame();
    }

    public class GamePlayController : IGamePlayController
    {
        private readonly IBuildingFactory _buildingFactory;
        private readonly IBuildingsViewRepository _buildingsViewRepository;
        private readonly GameContext _gameContext;
        private readonly IProcessingBuildingController _processingBuildingController;
        private readonly IMarketController _marketController;
        private readonly IResourceBuildingController _resourceBuildingController;


        public GamePlayController(GameContext gameContext,
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

        public void CreateGame()
        {
            var resourceBuildingCount = _gameContext.ResourceBuildingCount;
            for (var i = 0; i < resourceBuildingCount; i++)
            {
                var resourceBuilding = _buildingFactory.CreateResourceBuilding(i);
                _buildingsViewRepository.Add(resourceBuilding);
                resourceBuilding.OnBuildingClicked += _resourceBuildingController.ShowResourceBuildingWindow;
            }

            var processingBuilding = _buildingFactory.CreateProcessingBuilding();
            _buildingsViewRepository.Add(processingBuilding);
            processingBuilding.OnBuildingClicked += _processingBuildingController.ShowProcessingBuildingWindow;

            var market = _buildingFactory.CreateMarket();
            _buildingsViewRepository.Add(market);
            market.OnBuildingClicked += _marketController.ShowMarket;
        }
    }
}