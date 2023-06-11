using ProductionGame.Factories;
using ProductionGame.Models;

namespace ProductionGame.Controllers
{
    public interface IGamePlayController
    {
        void CreateGame();
    }

    public class GamePlayController : IGamePlayController
    {
        private readonly GameContext _gameContext;
        private readonly IBuildingFactory _buildingFactory;


        public GamePlayController(GameContext gameContext,
            IBuildingFactory buildingFactory)
        {
            _gameContext = gameContext;
            _buildingFactory = buildingFactory;
        }


        public void CreateGame()
        {
            var resourceBuildingCount = _gameContext.ResourceBuildingCount;
            for (var i = 0; i < resourceBuildingCount; i++)
            {
                var resourceBuilding = _buildingFactory.CreateResourceBuilding(i);
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
