using System;
using ProductionGame.Models;

namespace ProductionGame.GameView
{
    public class MarketBuildingView : BuildingView, IBuildingView<StorageModel>
    {
        private StorageModel _storageModel;
        public event Action<StorageModel> OnBuildingClicked;

        public void Initialize(StorageModel model)
        {
            _storageModel = model;
        }

        public override void Dispose()
        {
            OnBuildingClicked = null;
        }

        protected override void Click()
        {
            OnBuildingClicked?.Invoke(_storageModel);
        }
    }
}