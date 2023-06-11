using System;
using ProductionGame.Models;

namespace ProductionGame.GameView
{
    public class ResourceBuildingView : BuildingView, IBuildingView<ResourceBuildingModel>
    {
        private ResourceBuildingModel _resourceBuilding;
        public event Action<ResourceBuildingModel> OnBuildingClicked;

        public void Initialize(ResourceBuildingModel model)
        {
            _resourceBuilding = model;
        }

        public override void Dispose()
        {
            OnBuildingClicked = null;
        }


        protected override void Click()
        {
            OnBuildingClicked?.Invoke(_resourceBuilding);
        }
    }
}