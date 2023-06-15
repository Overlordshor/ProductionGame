using System.Collections.Generic;
using ProductionGame.Models;
using ProductionGame.SO;
using UnityEngine;

namespace ProductionGame.UI
{
    public interface IStorageView
    {
        void UpdateCount(ResourcesInfo resourceInfo, int count);
        void UpdateCoinCount(int count);
    }

    public class StorageView : MonoBehaviour, IStorageView
    {
        [SerializeField] private RectTransform _resourceGroup;
        [SerializeField] private ResourceStorageItemView _itemPrefab;
        [SerializeField] private bool _showOnStart;
        [SerializeField] private ResourceStorageItemView _coinView;

        private Dictionary<ResourceType, ResourceStorageItemView> _items = new();

        private void Start()
        {
            _itemPrefab.gameObject.SetActive(false);
            _resourceGroup.gameObject.SetActive(false);
            gameObject.SetActive(_showOnStart);
        }

        public void UpdateCoinCount(int count)
        {
            _coinView.SetCount(count);
        }

        public void UpdateCount(ResourcesInfo resourceInfo, int count)
        {
            if (!_items.ContainsKey(resourceInfo.ResourceType))
            {
                _resourceGroup.gameObject.SetActive(true);
                CreateItem(resourceInfo);
                return;
            }

            var itemView = _items[resourceInfo.ResourceType];
            itemView.SetCount(count);
        }

        private void CreateItem(ResourcesInfo resourceInfo)
        {
            var item = Instantiate(_itemPrefab, _resourceGroup);
            item.SetSprite(resourceInfo.Sprite);
            item.SetCount(1);
            item.gameObject.SetActive(true);
            _items[resourceInfo.ResourceType] = item;
        }
    }
}