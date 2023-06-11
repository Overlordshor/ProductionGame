using System;
using ProductionGame.Models;
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
        private readonly StorageModel _storageModel;

        private ProcessingBuildingModel _currentBuildingModel;

        public ProcessingBuildingController(IProcessingBuildingMenuView processingBuildingMenuView,
            StorageModel storageModel)
        {
            _processingBuildingMenuView = processingBuildingMenuView;
            _storageModel = storageModel;

            _processingBuildingMenuView.OnStartClicked += StartProduction;
            _processingBuildingMenuView.OnStopClicked += StopProduction;
        }

        public void ShowProcessingBuildingWindow(ProcessingBuildingModel processingBuilding)
        {
            _currentBuildingModel = processingBuilding;
            _processingBuildingMenuView.Show();
            _processingBuildingMenuView.SetStartButtonActive(!processingBuilding.IsProductionActive
                                                             && HasResource(
                                                                 processingBuilding.ResourceType1,
                                                                 processingBuilding.ResourceType2));
        }

        private void StartProduction()
        {
            if (_currentBuildingModel == null)
                return;

            if (_currentBuildingModel.IsProductionActive)
                return;

            _currentBuildingModel.SetResourceTypes(_processingBuildingMenuView.GetSelectedResourceType1(),
                _processingBuildingMenuView.GetSelectedResourceType2());

            if (!HasResource(_currentBuildingModel.ResourceType1, _currentBuildingModel.ResourceType2))
                throw new InvalidOperationException("Insufficient resources to start production.");

            _processingBuildingMenuView.SetStartButtonActive(false);
            _currentBuildingModel.StartProductionAsync().ConfigureAwait(false);
        }

        private bool HasResource(ResourceType resourceType1, ResourceType resourceType2)
        {
            return _storageModel.GetCount(resourceType1) > 0
                   && _storageModel.GetCount(resourceType2) > 0;
        }


        private void StopProduction()
        {
            if (_currentBuildingModel == null)
                return;

            _currentBuildingModel.StopProduction();
            _processingBuildingMenuView.SetStartButtonActive(!_currentBuildingModel.IsProductionActive
                                                             && HasResource(
                                                                 _currentBuildingModel.ResourceType1,
                                                                 _currentBuildingModel.ResourceType2));
        }
    }
}