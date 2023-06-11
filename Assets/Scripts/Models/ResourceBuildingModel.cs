using UnityEngine;

namespace ProductionGame.Models
{
    public class ResourceBuildingModel
    {
        private float _productionInterval;
        private float _timer;
        private int _currentResourceIndex;

        public ResourceType[] AvailableResources { get; }
        public bool IsProductionActive { get; private set; }

        public ResourceBuildingModel(ResourceType[] availableResources, float productionInterval)
        {
            AvailableResources = availableResources;
            _productionInterval = productionInterval;
            _timer = 0f;
            _currentResourceIndex = 0;
        }

        public void StartProduction()
        {
            IsProductionActive = true;
            _timer = 0f;
        }

        public void StopProduction()
        {
            IsProductionActive = false;
        }

        public void UpdateProduction()
        {
            if (!IsProductionActive)
                return;

            _timer += Time.deltaTime;

            if (_timer >= _productionInterval)
            {
                _timer -= _productionInterval;
                ProduceResource();
            }
        }

        private void ProduceResource()
        {
            var resourceType = AvailableResources[_currentResourceIndex];
            //TODO: storage

            _currentResourceIndex++;
            if (_currentResourceIndex >= AvailableResources.Length)
                _currentResourceIndex = 0;
        }
    }
}