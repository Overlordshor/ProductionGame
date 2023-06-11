using System.Collections.Generic;

namespace ProductionGame.Models
{
    public class StorageModel
    {
        private Dictionary<ResourceType, int> _resourceCounts;

        public StorageModel()
        {
            _resourceCounts = new Dictionary<ResourceType, int>();
        }

        public void AddResource(ResourceType resourceType, int count)
        {
            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType] += count;
            else
                _resourceCounts[resourceType] = count;
        }

        public int GetResourceCount(ResourceType resourceType)
        {
            return _resourceCounts.ContainsKey(resourceType)
                ? _resourceCounts[resourceType]
                : 0;
        }
    }
}