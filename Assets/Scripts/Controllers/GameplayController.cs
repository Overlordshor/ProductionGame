using ProductionGame.Factories;
using ProductionGame.Models;
using ProductionGame.Repositories;

namespace ProductionGame.Controllers
{
    public interface IGamePlayController
    {
        void CreateGame();
    }

    public class GamePlayController : IGamePlayController
    {
        private readonly IBuildingFactory _buildingFactory;
        private readonly GameContext _gameContext;
        private readonly IResourceBuildingController _resourceBuildingController;
        private readonly IBuildingsViewRepository _buildingsViewRepository;


        public GamePlayController(GameContext gameContext,
            IBuildingFactory buildingFactory,
            IResourceBuildingController resourceBuildingController,
            IBuildingsViewRepository buildingsViewRepository)
        {
            _gameContext = gameContext;
            _buildingFactory = buildingFactory;
            _resourceBuildingController = resourceBuildingController;
            _buildingsViewRepository = buildingsViewRepository;
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
            var market = _buildingFactory.CreateMarket();
            StartGame();
        }

        private void StartGame()
        {
        }
    }
}