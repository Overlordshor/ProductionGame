using System;
using System.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ResourceBuildingModel : IDisposable
    {
        public event Action<ResourceType> OnResourceProduced;
        public event Action OnDisposed;

        private ResourceType _currentResourceType;
        private float _productionInterval;

        public ResourceBuildingModel(float productionInterval)
        {
            _productionInterval = productionInterval;
        }

        public bool IsProductionActive { get; private set; }


        public async Task StartProductionAsync()
        {
            if (IsProductionActive)
                return;

            if (_currentResourceType == ResourceType.None)
                throw new InvalidOperationException();

            IsProductionActive = true;

            await ProduceResourcesAsync();
        }

        public void StopProduction()
        {
            IsProductionActive = false;
        }

        public void SetCurrentResource(ResourceType resourceType)
        {
            _currentResourceType = resourceType;
        }

        private async Task ProduceResourcesAsync()
        {
            while (IsProductionActive)
            {
                await Task.Delay(TimeSpan.FromSeconds(_productionInterval));
                OnResourceProduced?.Invoke(_currentResourceType);
            }
        }

        public void Dispose()
        {
            OnResourceProduced = null;
            OnDisposed?.Invoke();
        }
    }
}