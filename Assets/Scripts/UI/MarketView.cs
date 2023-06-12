using System;
using System.Linq;
using ProductionGame.Models;
using ProductionGame.SO;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IMarketView
    {
        event Action<ProductType> OnSellClicked;
        void Init(ProductInfo[] productsInfo);
        void Show(ProductType[] availableProducts);
    }

    public class MarketView : MonoBehaviour, IMarketView, IDisposable
    {
        public event Action<ProductType> OnSellClicked;

        [SerializeField] private Text _productText;
        [SerializeField] private Text _priceText;
        [SerializeField] private Button _sellButton;

        private ProductType[] _availableProducts;
        private int _currentProductIndex;
        private ProductInfo[] _productsInfo;
        private ProductInfo _currentProductInfo;

        public void Init(ProductInfo[] productsInfo)
        {
            _productsInfo = productsInfo;
        }

        public void Show(ProductType[] availableProducts)
        {
            _availableProducts = availableProducts;
            _currentProductIndex = 0;

            UpdateProductDisplay();

            // Register button click handler
            _sellButton.onClick.AddListener(HandleSellClicked);
        }

        private void UpdateProductDisplay()
        {
            // Display the current product and its price
            var currentProduct = _availableProducts[_currentProductIndex];
            _currentProductInfo = GetProductInfo(currentProduct);

            _productText.text = _currentProductInfo.Name;
            _priceText.text = "Price: " + _currentProductInfo.Price;
        }

        private void HandleSellClicked()
        {
            OnSellClicked?.Invoke(_currentProductInfo.ProductType);
            NextProduct();
        }

        private void NextProduct()
        {
            _currentProductIndex = (_currentProductIndex + 1) % _availableProducts.Length;
            UpdateProductDisplay();
        }

        private ProductInfo GetProductInfo(ProductType productType)
        {
            return _productsInfo.First(x => x.ProductType == productType);
        }

        private void OnDestroy()
        {
            _sellButton.onClick.RemoveListener(HandleSellClicked);
        }

        public void Dispose()
        {
            OnSellClicked = null;
        }
    }
}
