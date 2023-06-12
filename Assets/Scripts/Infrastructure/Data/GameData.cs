using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProductionGame.Models;

namespace ProductionGame.Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        public int CoinCount { get; private set; }
        public Dictionary<ResourceType, int> StorageResources { get; }
        public Dictionary<ProductType, int> StorageProducts { get; }

        [JsonConstructor]
        public GameData(int coinCount, Dictionary<ResourceType, int> storageResources,
            Dictionary<ProductType, int> storageProducts)
        {
            CoinCount = coinCount;
            StorageResources = storageResources;
            StorageProducts = storageProducts;
        }

        public void SetCoinsCount(int coins)
        {
            CoinCount = coins;
        }

        public void SetResourceCount(KeyValuePair<ResourceType, int> resource)
        {
            if (StorageResources.ContainsKey(resource.Key))
                StorageResources[resource.Key] = resource.Value;
            else
                StorageResources.Add(resource.Key, resource.Value);
        }

        public void SetProductsCount(KeyValuePair<ProductType, int> products)
        {
            if (StorageProducts.ContainsKey(products.Key))
                StorageProducts[products.Key] = products.Value;
            else
                StorageProducts.Add(products.Key, products.Value);
        }
    }
}