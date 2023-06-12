using System.Collections.Generic;
using System.Linq;

namespace ProductionGame.Models
{
    public class StorageModel
    {
        private Dictionary<ResourceType, int> _resourceCounts;
        private Dictionary<ProductType, int> _productCounts;

        public StorageModel()
        {
            _resourceCounts = new Dictionary<ResourceType, int>();
            _productCounts = new Dictionary<ProductType, int>();
        }

        public void Add(ResourceType resourceType, int count)
        {
            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType] += count;
            else
                _resourceCounts[resourceType] = count;
        }

        public void Add(ProductType productType, int count)
        {
            if (_productCounts.ContainsKey(productType))
                _productCounts[productType] += count;
            else
                _productCounts[productType] = count;
        }

        public int GetCount(ResourceType resourceType)
        {
            return _resourceCounts.ContainsKey(resourceType)
                ? _resourceCounts[resourceType]
                : 0;
        }

        public int GetCount(ProductType productType)
        {
            return _productCounts.ContainsKey(productType)
                ? _productCounts[productType]
                : 0;
        }

        public void Remove(ProductType productType)
        {
            if (_productCounts.ContainsKey(productType))
                _productCounts[productType]--;
        }

        public void Remove(ResourceType resourceType)
        {
            if (_resourceCounts.ContainsKey(resourceType))
                _resourceCounts[resourceType]--;
        }

        public ProductType[] GetAvailableProducts()
        {
            return _productCounts
                .Where(product => product.Value > 0)
                .Select(product => product.Key)
                .ToArray();
        }
    }
}
