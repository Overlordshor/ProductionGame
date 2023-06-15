using System;
using System.Collections.Generic;
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
        private readonly Dictionary<ResourceType, ResourcesInfo> _productsInfo;
        private StorageModel _storageModel;

        public MarketController(IMarketView marketView,
            PlayerModel playerModel,
            StorageModel storageModel,
            Dictionary<ResourceType, ResourcesInfo> productsInfo,
            IGameDataSaver gameDataSaver)
        {
            _playerModel = playerModel;
            _storageModel = storageModel;
            _productsInfo = productsInfo;
            _gameDataSaver = gameDataSaver;

            _marketView = marketView;
            _marketView.OnSellClicked += SellProduct;
            _marketView.OnNextProductSelected += SelectProduct;
        }

        public void ShowMarket(StorageModel storageModel)
        {
            _marketView.ClearCurrentResource();
            SetUpMarketView();

            _storageModel.OnResourcesChanged += UpdateAvailableProducts;
            _marketView.OnClosed += UnsubscribeUpdate;
            _marketView.Show();

            void UpdateAvailableProducts(ResourceType resourceType)
            {
                SetUpMarketView();
            }

            void UnsubscribeUpdate()
            {
                _marketView.OnClosed -= UnsubscribeUpdate;
                _storageModel.OnResourcesChanged -= UpdateAvailableProducts;
            }
        }


        public void Dispose()
        {
            _marketView.OnSellClicked -= SellProduct;
            _marketView.OnNextProductSelected -= SelectProduct;
        }

        private void SelectProduct(ResourcesInfo productInfo)
        {
            _marketView.SetCurrentProduct(productInfo.Name, productInfo.Price, productInfo.Sprite);
            _marketView.SetActiveSellButton(_storageModel.GetCount(productInfo.ResourceType) > 0);
        }

        private void SellProduct(ResourcesInfo productInfo)
        {
            if (_storageModel.GetCount(productInfo.ResourceType) <= 0)
                throw new InvalidOperationException();

            _storageModel.Remove(productInfo.ResourceType);
            _playerModel.AddCoins(productInfo.Price);

            var unavailableProducts = _storageModel.GetUnavailableProducts();
            _marketView.RemoveUnavailableProducts(unavailableProducts);
            var hasProducts = _storageModel.GetCount(productInfo.ResourceType) > 0;
            _marketView.SetActiveSellButton(hasProducts);
            if (!hasProducts)
                _marketView.ClearCurrentResource();

            _gameDataSaver.ChangeCoins(_playerModel.Coins);
            _gameDataSaver.Change(productInfo.ResourceType, _storageModel.GetCount(productInfo.ResourceType));
            _gameDataSaver.SaveChanges();
        }

        private void SetUpMarketView()
        {
            var availableProducts = _storageModel
                .GetAvailableResources()
                .Select(resource => _productsInfo[resource])
                .Where(productInfo => productInfo.Price > 0)
                .ToArray();

            _marketView.SetProducts(availableProducts);
            _marketView.SetActiveSellButton(availableProducts.Any());
        }
    }
}