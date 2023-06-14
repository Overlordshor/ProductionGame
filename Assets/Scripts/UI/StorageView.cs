using System.Collections.Generic;
using ProductionGame.Models;
using ProductionGame.SO;
using UnityEngine;

namespace ProductionGame.UI
{
    public interface IStorageView
    {
        void UpdateCount(ResourcesInfo resourceInfo, int count);
    }

    public class StorageView : MonoBehaviour, IStorageView
    {
        [SerializeField] private RectTransform _resourceGroup;
        [SerializeField] private ResourceStorageItemView _itemPrefab;
        [SerializeField] private bool _showOnStart;

        private Dictionary<ResourceType, ResourceStorageItemView> _items = new();

        private void Start()
        {
            _itemPrefab.gameObject.SetActive(false);
            gameObject.SetActive(_showOnStart);
        }

        public void UpdateCount(ResourcesInfo resourceInfo, int count)
        {
            if (!_items.ContainsKey(resourceInfo.ResourceType))
                CreateItem(resourceInfo);

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