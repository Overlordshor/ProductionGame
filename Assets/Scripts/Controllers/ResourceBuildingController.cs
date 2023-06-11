using ProductionGame.Models;
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

        public ResourceBuildingController(IResourceBuildingMenuView resourceBuildingMenuView)
        {
            _resourceBuildingMenuView = resourceBuildingMenuView;
            _resourceBuildingMenuView.OnCellClicked += SelectResource;
            _resourceBuildingMenuView.OnStartClicked += StartProduction;
            _resourceBuildingMenuView.OnStopClicked += StopProduction;

            _resourceBuildingMenuView.SetStartButtonActive(false);
        }

        private void SelectResource(ResourceBuildingModel resourceBuilding, ResourceType resourceType)
        {
            resourceBuilding.SetCurrentResource(resourceType);
            _resourceBuildingMenuView.SetStartButtonActive(!resourceBuilding.IsProductionActive);
        }

        public void ShowResourceBuildingWindow(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuildingMenuView.Show(resourceBuilding);
        }

        private void StartProduction(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuildingMenuView.SetStartButtonActive(false);
            resourceBuilding.StartProductionAsync().ConfigureAwait(false);
        }

        private void StopProduction(ResourceBuildingModel resourceBuilding)
        {
            resourceBuilding.StopProduction();
            _resourceBuildingMenuView.SetStartButtonActive(true);
        }
    }
}