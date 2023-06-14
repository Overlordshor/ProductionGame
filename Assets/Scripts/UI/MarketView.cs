using System;
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
        void SetCurrentResource(string resourceTitle, Sprite sprite);
        void ClearCurrentResource();
        void SetStartButtonState(bool value);
    }

    public class MarketView : MonoBehaviour, IMarketView, IDisposable
    {
        public event Action<ResourcesInfo> OnSellClicked;
        public event Action<ResourcesInfo> OnNextProductSelected;

        [SerializeField] private Button _nextProductButton;
        [SerializeField] private TextMeshProUGUI _productText;
        [SerializeField] private bool _showOnStart;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _closeButton;
        private Image _productImage;

        private ResourcesInfo[] _availableProducts;
        private int _currentProductIndex;

        private void Start()
        {
            _productImage = _nextProductButton.GetComponent<Image>();
            _nextProductButton.onClick.AddListener(SelectNextResource);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            _productImage = _nextProductButton.GetComponent<Image>();

            _sellButton.onClick.AddListener(HandleSellClicked);

            gameObject.SetActive(_showOnStart);
        }


        public void Show(ResourcesInfo[] availableProducts)
        {
            _availableProducts = availableProducts;
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            OnSellClicked = null;
            OnNextProductSelected = null;
        }

        public void SetCurrentResource(string resourceTitle, Sprite sprite)
        {
            _productText.text = resourceTitle;
            _productImage.sprite = sprite;
        }

        public void ClearCurrentResource()
        {
            SetCurrentResource(null, null);
            _currentProductIndex = 0;
        }

        public void SetStartButtonState(bool value)
        {
            _sellButton.interactable = value;
        }

        private void HandleSellClicked()
        {
            var selectedProduct = _availableProducts[_currentProductIndex];
            OnSellClicked?.Invoke(selectedProduct);
            SelectNextResource();
        }

        private void SelectNextResource()
        {
            _currentProductIndex = (_currentProductIndex + 1) % _availableProducts.Length;

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