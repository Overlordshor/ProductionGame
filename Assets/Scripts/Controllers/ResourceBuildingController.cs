using System.Collections.Generic;
using ProductionGame.Models;
using ProductionGame.SO;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IResourceBuildingController
    {
        void ShowResourceBuildingWindow(ResourceBuildingModel resourceBuilding);
    }

    public class ResourceBuildingController : IResourceBuildingController
    {
        private readonly IResourceBuildingMenuView _resourceBuildingMenuView;
        private readonly Dictionary<ResourceType, ResourcesInfo> _resourcesInfo;

        public ResourceBuildingController(IResourceBuildingMenuView resourceBuildingMenuView,
            Dictionary<ResourceType, ResourcesInfo> resourcesInfo)
        {
            _resourceBuildingMenuView = resourceBuildingMenuView;
            _resourcesInfo = resourcesInfo;
            _resourceBuildingMenuView.OnNextResourceSelected += SelectResource;
            _resourceBuildingMenuView.OnStartClicked += StartProduction;
            _resourceBuildingMenuView.OnStopClicked += StopProduction;

            _resourceBuildingMenuView.SetStartButtonState(false);
        }

        public void ShowResourceBuildingWindow(ResourceBuildingModel resourceBuilding)
        {
            if (!resourceBuilding.IsProductionActive)
            {
                resourceBuilding.SetCurrentResource(ResourceType.None);
                _resourceBuildingMenuView.ClearCurrentResource();
            }
            else
            {
                var info = _resourcesInfo[resourceBuilding.ResourceType];
                _resourceBuildingMenuView.SetCurrentResource(info.Name, info.Sprite);
            }

            _resourceBuildingMenuView.SetStartButtonState(!resourceBuilding.IsProductionActive);
            _resourceBuildingMenuView.Show(resourceBuilding);
        }

        private void SelectResource(ResourceBuildingModel resourceBuilding, ResourceType resourceType)
        {
            var info = _resourcesInfo[resourceBuilding.ResourceType];
            resourceBuilding.SetCurrentResource(resourceType);
            _resourceBuildingMenuView.SetCurrentResource(info.Name, info.Sprite);
            _resourceBuildingMenuView.SetStartButtonState(!resourceBuilding.IsProductionActive);
        }


        private void StartProduction(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuildingMenuView.SetStartButtonState(false);
            resourceBuilding.StartProductionAsync().ConfigureAwait(false);
        }

        private void StopProduction(ResourceBuildingModel resourceBuilding)
        {
            resourceBuilding.StopProduction();
            _resourceBuildingMenuView.SetStartButtonState(true);
        }
    }
}