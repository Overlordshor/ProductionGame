using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductionGame.Models
{
    public class StorageModel : IDisposable
    {
        public event Action<ResourceType> OnResourcesChanged;

        private readonly Dictionary<ResourceType, int> _resourceCounts;

        public StorageModel(Dictionary<ResourceType, int> initStorageResources)
        {
            _resourceCounts = initStorageResources;
        }

        public void Add(ResourceType resourceType, int count)
        {
            if (resourceType == ResourceType.None)
                throw new ArgumentOutOfRangeException();

            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType] += count;
            else
                _resourceCounts[resourceType] = count;

            OnResourcesChanged?.Invoke(resourceType);
        }

        public int GetCount(ResourceType resourceType)
        {
            return _resourceCounts.ContainsKey(resourceType)
                ? _resourceCounts[resourceType]
                : 0;
        }

        public void Remove(ResourceType resourceType)
        {
            if (resourceType == ResourceType.None)
                throw new ArgumentOutOfRangeException();

            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType]--;

            OnResourcesChanged?.Invoke(resourceType);
        }

        public IEnumerable<ResourceType> GetAvailableResources()
        {
            return _resourceCounts
                .Where(product => product.Value > 0)
                .Select(product => product.Key);
        }

        public IEnumerable<ResourceType> GetUnavailableProducts()
        {
            return _resourceCounts
                .Where(product => product.Value <= 0)
                .Select(product => product.Key);
        }

        public bool HasResource(params ResourceType[] resources)
        {
            return resources.All(resource => GetCount(resource) > 0);
        }

        public void Dispose()
        {
            OnResourcesChanged = null;
            ;
        }
    }
}
