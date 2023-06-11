using System;
using System.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ProcessingBuildingModel : IDisposable
    {
        public event Action<ProductType> OnProductProduced;
        public event Action OnDisposed;

        private ProductType _currentProductType;
        private float _productionInterval;


        public ProcessingBuildingModel(float productionInterval)
        {
            _productionInterval = productionInterval;
        }

        public bool IsProductionActive { get; private set; }
        public ResourceType ResourceType1 { get; private set; }

        public ResourceType ResourceType2 { get; private set; }

        public void SetResourceTypes(ResourceType resourceType1, ResourceType resourceType2)
        {
            ResourceType1 = resourceType1;
            ResourceType2 = resourceType2;
            CalculateProductType();
        }

        public async Task StartProductionAsync()
        {
            if (IsProductionActive || _currentProductType == ProductType.None)
                return;

            IsProductionActive = true;

            await ProduceProductsAsync();
        }

        public void StopProduction()
        {
            IsProductionActive = false;
        }

        private async Task ProduceProductsAsync()
        {
            while (IsProductionActive)
            {
                await Task.Delay(TimeSpan.FromSeconds(_productionInterval));
                OnProductProduced?.Invoke(_currentProductType);
            }
        }

        private void CalculateProductType()
        {
            _currentProductType = ResourceType1 switch
            {
                ResourceType.Wood when ResourceType2 == ResourceType.Stone => ProductType.Hammers,
                ResourceType.Wood when ResourceType2 == ResourceType.Iron => ProductType.Forks,
                ResourceType.Stone when ResourceType2 == ResourceType.Iron => ProductType.Drills,
                _ => ProductType.None
            };
        }


        public void Dispose()
        {
            OnProductProduced = null;
            OnDisposed?.Invoke();
        }
    }
}
