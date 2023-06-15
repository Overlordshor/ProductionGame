using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProductionGame.Models;
using ProductionGame.SO;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IProcessingBuildingController
    {
        void ShowProcessingBuildingWindow(ProcessingBuildingModel processingBuilding);
    }

    public class ProcessingBuildingController : IProcessingBuildingController, IDisposable
    {
        private readonly IProcessingBuildingMenuView _processingBuildingMenuView;
        private readonly Dictionary<ResourceType, ResourcesInfo> _resourcesInfo;
        private readonly StorageModel _storageModel;

        private ProcessingBuildingModel _processingBuilding;

        public ProcessingBuildingController(IProcessingBuildingMenuView processingBuildingMenuView,
            StorageModel storageModel,
            Dictionary<ResourceType, ResourcesInfo> resourcesInfo)
        {
            _processingBuildingMenuView = processingBuildingMenuView;
            _storageModel = storageModel;
            _resourcesInfo = resourcesInfo;

            _processingBuildingMenuView.OnStartClicked += StartOrStopProduction;
            _processingBuildingMenuView.OnResourcesSelected += SelectResource;
            _storageModel.OnResourcesChanged += UpdateState;
        }

        public void ShowProcessingBuildingWindow(ProcessingBuildingModel processingBuilding)
        {
            _processingBuilding = processingBuilding;
            _processingBuildingMenuView.Show();
            UpdateStartButton();
        }

        private void SelectResource(ResourceType resource1Type, ResourceType resource2Type)
        {
            _processingBuilding.SetResourceTypes(resource1Type, resource2Type);


            var resource1Info = GetResourceInfo(_processingBuilding.ResourceType1);
            var resource2Info = GetResourceInfo(_processingBuilding.ResourceType2);
            var productInfo = GetResourceInfo(_processingBuilding.ProductType);


            _processingBuildingMenuView.SetCurrentCraftView(new[] { resource1Info?.Sprite, resource2Info?.Sprite },
                productInfo?.Sprite);

            UpdateStartButton();
        }

        private ResourcesInfo GetResourceInfo(ResourceType resourceType)
        {
            return _resourcesInfo.ContainsKey(resourceType)
                ? _resourcesInfo[resourceType]
                : null;
        }

        private void StartOrStopProduction(ResourceType resource1Type, ResourceType resource2Type)
        {
            if (!_processingBuilding.IsProductionActive)
            {
                if (!_storageModel.HasResource(_processingBuilding.ResourceType1, _processingBuilding.ResourceType2))
                    throw new InvalidOperationException("Insufficient resources to start production.");

                _processingBuilding.OnResourcesConsumed += UpdateState;
                _processingBuilding.StartProductionAsync().Forget();
            }
            else
            {
                _processingBuilding.OnResourcesConsumed -= UpdateState;
                _processingBuilding.StopProduction();
            }

            UpdateStartButton();
        }

        private void UpdateState(ResourceType resourceType)
        {
            if (_processingBuilding == null)
                return;

            if (_processingBuilding.ProductType != resourceType
                && !_storageModel.HasResource(resourceType))
                _processingBuilding.StopProduction();

            UpdateStartButton();
        }

        private void UpdateStartButton()
        {
            if (!_processingBuilding.IsProductionActive)
                _processingBuildingMenuView.SetActiveStartButton(_storageModel.HasResource(
                                                                     _processingBuilding.ResourceType1,
                                                                     _processingBuilding.ResourceType2)
                                                                 && _processingBuilding.ProductType !=
                                                                 ResourceType.None);

            _processingBuildingMenuView.SetStartButtonState(_processingBuilding.IsProductionActive);
        }

        public void Dispose()
        {
            _processingBuildingMenuView.OnStartClicked -= StartOrStopProduction;
            _processingBuildingMenuView.OnResourcesSelected -= SelectResource;
            _storageModel.OnResourcesChanged -= UpdateState;

            if (_processingBuilding != null)
                _processingBuilding.OnResourcesConsumed -= UpdateState;
        }
    }
}