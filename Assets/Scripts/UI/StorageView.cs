using System.Collections.Generic;
using ProductionGame.Models;
using TMPro;
using UnityEngine;

namespace ProductionGame.UI
{
    public interface IStorageView
    {
        void UpdateCount(ResourceType resourceType, int count);
        void UpdateCount(ProductType productType, int count);
    }

    public class StorageView : MonoBehaviour, IStorageView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _itemPrefab;

        private readonly Dictionary<ResourceType, TextMeshProUGUI> _resourceTexts = new();
        private readonly Dictionary<ProductType, TextMeshProUGUI> _productsTexts = new();

        public void UpdateCount(ResourceType resourceType, int count)
        {
            if (!_resourceTexts.ContainsKey(resourceType))
                CreateItem(resourceType);

            var resourceText = _resourceTexts[resourceType];
            resourceText.text = $"{resourceType}: {count}";
        }

        public void UpdateCount(ProductType productType, int count)
        {
            if (!_productsTexts.ContainsKey(productType))
                CreateItem(productType);

            var resourceText = _productsTexts[productType];
            resourceText.text = $"{productType}: {count}";
        }

        private void CreateItem(ResourceType resourceType)
        {
            var item = Instantiate(_itemPrefab, _content);
            item.text = $"{resourceType}: 0";
            _resourceTexts[resourceType] = item;
        }

        private void CreateItem(ProductType productType)
        {
            var item = Instantiate(_itemPrefab, _content);
            item.text = $"{productType}: 0";
            _productsTexts[productType] = item;
        }
    }
}