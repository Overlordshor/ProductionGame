using System;
using System.Linq;
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
        private readonly IMarketView _marketView;
        private readonly PlayerModel _playerModel;
        private readonly ProductInfo[] _productsInfo;
        private StorageModel _storageModel;

        public MarketController(IMarketView marketView,
            PlayerModel playerModel,
            ProductInfo[] productsInfo)
        {
            _playerModel = playerModel;
            _productsInfo = productsInfo;

            _marketView = marketView;
            _marketView.Init(productsInfo);
            _marketView.OnSellClicked += SellProduct;
        }

        public void ShowMarket(StorageModel storageModel)
        {
            _storageModel = storageModel;
            var availableProducts = _storageModel.GetAvailableProducts();
            _marketView.Show(availableProducts);
        }

        private void SellProduct(ProductType productType)
        {
            var productInfo = _productsInfo.First(info => info.ProductType == productType);

            if (_storageModel.GetCount(productType) <= 0)
                return;

            _storageModel.Remove(productType);
            _playerModel.AddCoins(productInfo.Price);
        }

        public void Dispose()
        {
            _marketView.OnSellClicked -= SellProduct;
        }
    }
}