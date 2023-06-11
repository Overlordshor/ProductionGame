using System;
using UnityEngine;

namespace ProductionGame.Models
{
    public class ResourceBuildingModel
    {
        private float _productionInterval;
        private float _timer;
        private int _currentResourceIndex;
        public bool IsProductionActive { get; private set; }

        public ResourceBuildingModel(float productionInterval)
        {
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
            var resourceType = (ResourceType)_currentResourceIndex;
            //TODO: storage

            if (resourceType == ResourceType.None)
                throw new InvalidCastException();

            _currentResourceIndex++;
        }
    }
}