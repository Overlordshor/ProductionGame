using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ResourceBuildingModel : IDisposable
    {
        public event Action<ResourceType> OnResourceProduced;
        public event Action OnDisposed;

        private float _productionInterval;
        private CancellationTokenSource _ctx;
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

            _ctx = new CancellationTokenSource();
            await ProduceResourcesAsync(_ctx.Token);
        }

        public void StopProduction()
        {
            _ctx.Cancel();
            IsProductionActive = false;
        }

        public void SetCurrentResource(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        private async UniTask ProduceResourcesAsync(CancellationToken cancellationToken)
        {
            while (IsProductionActive && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_productionInterval), cancellationToken: cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    return;

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