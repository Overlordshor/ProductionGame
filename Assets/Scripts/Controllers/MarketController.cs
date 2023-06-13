using System;
using System.Linq;
using ProductionGame.Infrastructure;
using ProductionGame.Models;
using ProductionGame.SO;

namespace ProductionGame.UI
{
    public interface IMarketController
    {
        void ShowMarket(StorageModel storageModel);
    }

    public class MarketController : IMarketController, IDisposable
    {
        private readonly IGameDataSaver _gameDataSaver;
        private readonly IMarketView _marketView;
        private readonly PlayerModel _playerModel;
        private readonly ResourcesInfo[] _productsInfo;
        private StorageModel _storageModel;

        public MarketController(IMarketView marketView,
            PlayerModel playerModel,
            ResourcesInfo[] productsInfo,
            IGameDataSaver gameDataSaver)
        {
            _playerModel = playerModel;
            _productsInfo = productsInfo;
            _gameDataSaver = gameDataSaver;

            _marketView = marketView;
            _marketView.Init(productsInfo);
            _marketView.OnSellClicked += SellProduct;
        }

        public void ShowMarket(StorageModel storageModel)
        {
            _storageModel = storageModel;
            var availableProducts = _storageModel
                .GetAvailableResources()
                .Where(resource =>
                {
                    var productInfo = _productsInfo.First(info => info.ResourceType == resource);
                    return productInfo.Price > 0;
                }).ToArray();

            _marketView.Show(availableProducts);
        }

        public void Dispose()
        {
            _marketView.OnSellClicked -= SellProduct;
        }

        private void SellProduct(ResourceType resourceType)
        {
            var productInfo = _productsInfo.First(info => info.ResourceType == resourceType);

            if (_storageModel.GetCount(resourceType) <= 0)
                return;

            _storageModel.Remove(resourceType);
            _playerModel.AddCoins(productInfo.Price);

            _gameDataSaver.ChangeCoins(_playerModel.Coins);
            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }
    }
}