using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
            _resourceBuildingMenuView.OnStartClicked += StartOrStopProduction;

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

            _resourceBuildingMenuView.SetStartButtonState(resourceBuilding.IsProductionActive);
            _resourceBuildingMenuView.Show(resourceBuilding);
        }

        private void SelectResource(ResourceBuildingModel resourceBuilding, ResourceType resourceType)
        {
            resourceBuilding.SetCurrentResource(resourceType);

            var info = _resourcesInfo[resourceBuilding.ResourceType];
            _resourceBuildingMenuView.SetCurrentResource(info.Name, info.Sprite);
            _resourceBuildingMenuView.SetStartButtonState(resourceBuilding.IsProductionActive);
        }


        private void StartOrStopProduction(ResourceBuildingModel resourceBuilding)
        {
            var isProductionActive = resourceBuilding.IsProductionActive;
            if (isProductionActive)
                resourceBuilding.StopProduction();
            else
                resourceBuilding.StartProductionAsync().Forget();

            _resourceBuildingMenuView.SetStartButtonState(resourceBuilding.IsProductionActive);
        }
    }
}