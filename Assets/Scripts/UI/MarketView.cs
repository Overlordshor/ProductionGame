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
        event Action<ResourceType> OnSellClicked;
        void Init(ResourcesInfo[] productsInfo);
        void Show(ResourceType[] availableProducts);
    }

    public class MarketView : MonoBehaviour, IMarketView, IDisposable
    {
        private ResourceType[] _availableProducts;
        private int _currentProductIndex;
        private ResourcesInfo _currentResourcesInfo;
        [SerializeField] private Text _priceText;
        private ResourcesInfo[] _productsInfo;

        [SerializeField] private Text _productText;
        [SerializeField] private Button _sellButton;
        [SerializeField] private bool showOnStart;

        public void Dispose()
        {
            OnSellClicked = null;
        }

        public event Action<ResourceType> OnSellClicked;

        public void Init(ResourcesInfo[] productsInfo)
        {
            _productsInfo = productsInfo;
        }

        public void Show(ResourceType[] availableProducts)
        {
            _availableProducts = availableProducts;
            _currentProductIndex = 0;

            UpdateProductDisplay();

            // Register button click handler
            _sellButton.onClick.AddListener(HandleSellClicked);
        }

        private void Start()
        {
            gameObject.SetActive(showOnStart);
        }

        private void UpdateProductDisplay()
        {
            // Display the current product and its price
            var currentProduct = _availableProducts[_currentProductIndex];
            _currentResourcesInfo = GetProductInfo(currentProduct);

            _productText.text = _currentResourcesInfo.Name;
            _priceText.text = "Price: " + _currentResourcesInfo.Price;
        }

        private void HandleSellClicked()
        {
            OnSellClicked?.Invoke(_currentResourcesInfo.ResourceType);
            NextProduct();
        }

        private void NextProduct()
        {
            _currentProductIndex = (_currentProductIndex + 1) % _availableProducts.Length;
            UpdateProductDisplay();
        }

        private ResourcesInfo GetProductInfo(ResourceType ResourceType)
        {
            return _productsInfo.First(x => x.ResourceType == ResourceType);
        }

        private void OnDestroy()
        {
            _sellButton.onClick.RemoveListener(HandleSellClicked);
        }
    }
}