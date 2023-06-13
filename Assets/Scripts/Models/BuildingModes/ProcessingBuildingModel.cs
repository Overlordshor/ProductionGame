using System;
using System.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ProcessingBuildingModel : IDisposable
    {
        public event Action<ResourceType> OnResourcesConsumed;
        public event Action<ResourceType> OnProductProduced;
        public event Action OnDisposed;

        private ResourceType _currentResourceType;
        private float _productionInterval;
        private readonly StorageModel _storageModel;

        public bool IsProductionActive { get; private set; }
        public ResourceType ResourceType1 { get; private set; }
        public ResourceType ResourceType2 { get; private set; }

        public ProcessingBuildingModel(float productionInterval, StorageModel storageModel)
        {
            _productionInterval = productionInterval;
            _storageModel = storageModel;
        }

        public void SetResourceTypes(ResourceType resourceType1, ResourceType resourceType2)
        {
            ResourceType1 = resourceType1;
            ResourceType2 = resourceType2;
            CalculateResourceType();
        }

        public async Task StartProductionAsync()
        {
            if (IsProductionActive || _currentResourceType == ResourceType.None)
                return;

            IsProductionActive = true;

            await ProduceProductsAsync();
            StopProduction();
        }

        public void StopProduction()
        {
            IsProductionActive = _storageModel.HasResource(ResourceType1, ResourceType2);
        }

        private async Task ProduceProductsAsync()
        {
            while (IsProductionActive
                   && !_storageModel.HasResource(ResourceType1, ResourceType2))
            {
                await Task.Delay(TimeSpan.FromSeconds(_productionInterval));

                OnResourcesConsumed?.Invoke(ResourceType1);
                OnResourcesConsumed?.Invoke(ResourceType2);
                OnProductProduced?.Invoke(_currentResourceType);
            }
        }


        private void CalculateResourceType()
        {
            _currentResourceType = ResourceType1 switch
            {
                ResourceType.Wood when ResourceType2 == ResourceType.Stone => ResourceType.Hammers,
                ResourceType.Wood when ResourceType2 == ResourceType.Iron => ResourceType.Forks,
                ResourceType.Stone when ResourceType2 == ResourceType.Iron => ResourceType.Drills,
                _ => ResourceType.None
            };
        }

        public void Dispose()
        {
            OnResourcesConsumed = null;
            OnProductProduced = null;
            OnDisposed?.Invoke();
            OnDisposed = null;
        }
    }
}
