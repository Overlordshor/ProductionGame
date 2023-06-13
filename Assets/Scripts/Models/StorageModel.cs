using System.Collections.Generic;
using System.Linq;

namespace ProductionGame.Models
{
    public class StorageModel
    {
        private Dictionary<ResourceType, int> _resourceCounts;

        public StorageModel()
        {
            _resourceCounts = new Dictionary<ResourceType, int>();
        }

        public void Add(ResourceType resourceType, int count)
        {
            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType] += count;
            else
                _resourceCounts[resourceType] = count;
        }

        public int GetCount(ResourceType resourceType)
        {
            return _resourceCounts.ContainsKey(resourceType)
                ? _resourceCounts[resourceType]
                : 0;
        }

        public void Remove(ResourceType resourceType)
        {
            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType]--;
        }

        public IEnumerable<ResourceType> GetAvailableResources()
        {
            return _resourceCounts
                .Where(product => product.Value > 0)
                .Select(product => product.Key);
        }

        public bool HasResource(params ResourceType[] resources)
        {
            return resources.All(resource => GetCount(resource) > 0);
        }
    }
}
