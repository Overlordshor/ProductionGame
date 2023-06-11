using System.Collections.Generic;
using ProductionGame.Models;
using TMPro;
using UnityEngine;

namespace ProductionGame.UI
{
    public interface IStorageView
    {
        void UpdateResourceCount(ResourceType resourceType, int count);
    }

    public class StorageView : MonoBehaviour, IStorageView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _resourceItemPrefab;

        private readonly Dictionary<ResourceType, TextMeshProUGUI> _resourceTexts = new();

        public void UpdateResourceCount(ResourceType resourceType, int count)
        {
            if (!_resourceTexts.ContainsKey(resourceType))
                CreateResourceItem(resourceType);

            var resourceText = _resourceTexts[resourceType];
            resourceText.text = $"{resourceType}: {count}";
        }

        private void CreateResourceItem(ResourceType resourceType)
        {
            var resourceItem = Instantiate(_resourceItemPrefab, _content);
            resourceItem.text = $"{resourceType}: 0";
            _resourceTexts[resourceType] = resourceItem;
        }
    }
}