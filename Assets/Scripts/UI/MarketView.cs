using System;
using System.Collections.Generic;
using System.Linq;
using ProductionGame.Models;
using ProductionGame.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IMarketView
    {
        event Action<ResourcesInfo> OnSellClicked;
        event Action<ResourcesInfo> OnNextProductSelected;
        void Show(ResourcesInfo[] availableProducts);
        void SetCurrentProduct(string resourceTitle, int price, Sprite sprite);
        void ClearCurrentResource();
        void SetActiveSellButton(bool value);
        void RemoveUnavailableProducts(IEnumerable<ResourceType> unavailableProducts);
    }

    public class MarketView : View, IMarketView, IDisposable
    {
        public event Action<ResourcesInfo> OnSellClicked;
        public event Action<ResourcesInfo> OnNextProductSelected;

        [SerializeField] private Button _nextProductButton;
        [SerializeField] private TextMeshProUGUI _productText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _closeButton;
        private Image _productImage;

        private List<ResourcesInfo> _availableProducts;
        private int _currentProductIndex;

        protected override void OnStart()
        {
            _productImage = _nextProductButton.GetComponent<Image>();
            _nextProductButton.onClick.AddListener(SelectNextResource);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            _productImage = _nextProductButton.GetComponent<Image>();

            _sellButton.onClick.AddListener(HandleSellClicked);
        }


        public void Show(ResourcesInfo[] availableProducts)
        {
            _availableProducts = availableProducts.ToList();
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            OnSellClicked = null;
            OnNextProductSelected = null;
        }

        public void SetCurrentProduct(string title, int price, Sprite sprite)
        {
            _productText.text = title;
            _productImage.sprite = sprite;
            _priceText.text = price.ToString();
            _priceText.gameObject.SetActive(price > 0);
        }

        public void ClearCurrentResource()
        {
            SetCurrentProduct(null, 0, null);
            _currentProductIndex = 0;
        }

        public void SetActiveSellButton(bool value)
        {
            _sellButton.interactable = value;
        }

        public void RemoveUnavailableProducts(IEnumerable<ResourceType> unavailableProducts)
        {
            var count = _availableProducts
                .RemoveAll(resourcesInfo => unavailableProducts
                    .Contains(resourcesInfo.ResourceType));
            if (count == 0)
                return;

            ClearCurrentResource();
        }

        private void HandleSellClicked()
        {
            var selectedProduct = _availableProducts[_currentProductIndex];
            OnSellClicked?.Invoke(selectedProduct);
        }

        private void SelectNextResource()
        {
            if (_availableProducts.Count == 0)
            {
                ClearCurrentResource();
                return;
            }

            _currentProductIndex = (_currentProductIndex + 1) % _availableProducts.Count;
            _currentProductIndex = Math.Min(_currentProductIndex, _availableProducts.Count);
            if (_currentProductIndex >= _availableProducts.Count)
                return;

            var selectedProduct = _availableProducts[_currentProductIndex];
            OnNextProductSelected?.Invoke(selectedProduct);
        }

        private void OnDestroy()
        {
            _sellButton.onClick.RemoveAllListeners();
            _nextProductButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}