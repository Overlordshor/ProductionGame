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

        [JsonConstructor]
        public GameData(int coinCount, Dictionary<ResourceType, int> storageResources)
        {
            CoinCount = coinCount;
            StorageResources = storageResources;
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
    }
}