using System;
using Cysharp.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ResourceBuildingModel : IDisposable
    {
        public event Action<ResourceType> OnResourceProduced;
        public event Action OnDisposed;

        private float _productionInterval;
        public ResourceType ResourceType { get; private set; }

        public ResourceBuildingModel(float productionInterval)
        {
            _productionInterval = productionInterval;
        }

        public bool IsProductionActive { get; private set; }


        public async UniTask StartProductionAsync()
        {
            if (IsProductionActive)
                return;

            if (ResourceType == ResourceType.None)
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
            ResourceType = resourceType;
        }

        private async UniTask ProduceResourcesAsync()
        {
            while (IsProductionActive)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_productionInterval));
                OnResourceProduced?.Invoke(ResourceType);
            }
        }

        public void Dispose()
        {
            OnResourceProduced = null;
            OnDisposed?.Invoke();
        }
    }
}