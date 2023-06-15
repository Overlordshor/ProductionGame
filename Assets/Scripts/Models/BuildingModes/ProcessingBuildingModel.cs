using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ProductionGame.Models
{
    public class ProcessingBuildingModel : IDisposable
    {
        public event Action<ResourceType> OnResourcesConsumed;
        public event Action<ResourceType> OnProductProduced;
        public event Action OnDisposed;

        private readonly StorageModel _storageModel;
        private Craft _craft;
        private float _productionInterval;
        private CancellationTokenSource _ctx;

        public bool IsProductionActive { get; private set; }
        public ResourceType ResourceType1 => _craft?.Resources[0] ?? ResourceType.None;
        public ResourceType ResourceType2 => _craft?.Resources[1] ?? ResourceType.None;
        public ResourceType ProductType => _craft?.Product ?? ResourceType.None;


        public ProcessingBuildingModel(float productionInterval, StorageModel storageModel)
        {
            _productionInterval = productionInterval;
            _storageModel = storageModel;
        }

        public void SetResourceTypes(ResourceType resourceType1, ResourceType resourceType2)
        {
            _craft = new Craft(new[] { resourceType1, resourceType2 });
        }

        public async UniTask StartProductionAsync()
        {
            if (IsProductionActive || ProductType == ResourceType.None)
                return;

            IsProductionActive = true;

            _ctx = new CancellationTokenSource();
            await ProduceProductsAsync(_ctx.Token);
        }

        public void StopProduction()
        {
            _ctx?.Cancel();
            IsProductionActive = false;
        }

        public void Dispose()
        {
            OnResourcesConsumed = null;
            OnProductProduced = null;
            OnDisposed?.Invoke();
            OnDisposed = null;
        }

        private async UniTask ProduceProductsAsync(CancellationToken cancellationToken)
        {
            while (IsProductionActive
                   && _storageModel.HasResource(_craft.Resources)
                   && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_productionInterval), cancellationToken: cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    return;

                OnResourcesConsumed?.Invoke(ResourceType1);
                OnResourcesConsumed?.Invoke(ResourceType2);
                OnProductProduced?.Invoke(ProductType);
            }
        }
    }
}