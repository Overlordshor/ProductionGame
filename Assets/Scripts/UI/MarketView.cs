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
        event Action OnClosed;
        void Show();
        void SetCurrentProduct(string resourceTitle, int price, Sprite sprite);
        void ClearCurrentResource();
        void SetActiveSellButton(bool value);
        void RemoveUnavailableProducts(IEnumerable<ResourceType> unavailableProducts);
        void SetProducts(ResourcesInfo[] availableProducts);
    }

    public class MarketView : View, IMarketView, IDisposable
    {
        public event Action<ResourcesInfo> OnSellClicked;
        public event Action<ResourcesInfo> OnNextProductSelected;
        public event Action OnClosed;

        [SerializeField] private Button _nextProductButton;
        [SerializeField] private TextMeshProUGUI _productText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _closeButton;
        private Image _productImage;

        private List<ResourcesInfo> _availableProducts;
        private int _currentProductIndex;
        private Sprite _defaultSprite;

        protected override void OnStart()
        {
            _productImage = _nextProductButton.GetComponent<Image>();
            _defaultSprite = _productImage.sprite;

            _nextProductButton.onClick.AddListener(SelectNextResource);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));


            _sellButton.onClick.AddListener(HandleSellClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetProducts(ResourcesInfo[] availableProducts)
        {
            _availableProducts = availableProducts.ToList();
        }

        public void Dispose()
        {
            OnClosed = null;
            OnSellClicked = null;
            OnNextProductSelected = null;
        }

        public void SetCurrentProduct(string title, int price, Sprite sprite)
        {
            _productText.text = title;
            _productImage.sprite = sprite ?? _defaultSprite;
            _priceText.text = price.ToString();
            _priceText.gameObject.SetActive(price > 0);
        }

        public void ClearCurrentResource()
        {
            SetCurrentProduct(null, 0, null);
            _currentProductIndex = -1;
        }

        public void SetActiveSellButton(bool value)
        {
            _sellButton.interactable = value && _currentProductIndex >= 0;
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

        protected override void OnDisable()
        {
            base.OnDisable();
            OnClosed?.Invoke();
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