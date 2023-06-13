using System.Linq;
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
        private readonly ResourcesInfo[] _resourcesInfo;

        public ResourceBuildingController(IResourceBuildingMenuView resourceBuildingMenuView,
            ResourcesInfo[] resourcesInfo)
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
                var info = GetResourcesInfo(resourceBuilding.ResourceType);
                _resourceBuildingMenuView.SetCurrentResource(info.Name, info.Sprite);
            }

            _resourceBuildingMenuView.SetStartButtonState(!resourceBuilding.IsProductionActive);
            _resourceBuildingMenuView.Show(resourceBuilding);
        }

        private void SelectResource(ResourceBuildingModel resourceBuilding, ResourceType resourceType)
        {
            var info = GetResourcesInfo(resourceBuilding.ResourceType);
            resourceBuilding.SetCurrentResource(resourceType);
            _resourceBuildingMenuView.SetCurrentResource(info.Name, info.Sprite);
            _resourceBuildingMenuView.SetStartButtonState(!resourceBuilding.IsProductionActive);
        }

        private ResourcesInfo GetResourcesInfo(ResourceType resourceType)
        {
            return _resourcesInfo.First(info => info.ResourceType == resourceType);
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