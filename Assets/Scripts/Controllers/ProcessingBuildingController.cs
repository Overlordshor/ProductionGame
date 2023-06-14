using System;
using System.Collections.Generic;
using ProductionGame.Models;
using ProductionGame.SO;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IProcessingBuildingController
    {
        void ShowProcessingBuildingWindow(ProcessingBuildingModel processingBuilding);
    }

    public class ProcessingBuildingController : IProcessingBuildingController
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

            _processingBuildingMenuView.OnStartClicked += StartProduction;
            _processingBuildingMenuView.OnResourcesSelected += SelectResource;
            _processingBuildingMenuView.OnStopClicked += StopProduction;
        }

        public void ShowProcessingBuildingWindow(ProcessingBuildingModel processingBuilding)
        {
            _processingBuilding = processingBuilding;
            _processingBuildingMenuView.Show();
            _processingBuildingMenuView.SetStartButtonState(!processingBuilding.IsProductionActive
                                                            && _storageModel.HasResource(
                                                                processingBuilding.ResourceType1,
                                                                processingBuilding.ResourceType2));
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

        private void StartProduction(ResourceType resource1Type, ResourceType resource2Type)
        {
            if (_processingBuilding.IsProductionActive)
                throw new InvalidOperationException();

            if (!_storageModel.HasResource(_processingBuilding.ResourceType1, _processingBuilding.ResourceType2))
                throw new InvalidOperationException("Insufficient resources to start production.");

            _processingBuilding.StartProductionAsync().ConfigureAwait(false);
            UpdateStartButton();
        }


        private void StopProduction()
        {
            if (_processingBuilding == null)
                return;

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

            _processingBuildingMenuView.SetStartButtonState(!_processingBuilding.IsProductionActive);
        }
    }
}