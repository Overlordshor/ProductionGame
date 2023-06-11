using System;
using ProductionGame.Models;

namespace ProductionGame.GameView
{
    public class ProcessingBuildingView : BuildingView, IBuildingView<ProcessingBuildingModel>
    {
        private ProcessingBuildingModel _processingBuilding;
        public event Action<ProcessingBuildingModel> OnBuildingClicked;

        public void Initialize(ProcessingBuildingModel model)
        {
            _processingBuilding = model;
        }

        public override void Dispose()
        {
            OnBuildingClicked = null;
        }

        protected override void Click()
        {
            OnBuildingClicked?.Invoke(_processingBuilding);
        }
    }
}