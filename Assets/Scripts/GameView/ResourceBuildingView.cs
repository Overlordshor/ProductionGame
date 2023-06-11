using System;
using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.GameView
{
    public class ResourceBuildingView : MonoBehaviour, IBuildingView<ResourceBuildingModel>
    {
        public event Action<ResourceBuildingModel> OnBuildingClicked;

        private ResourceBuildingModel _resourceBuilding;

        public void Initialize(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuilding = resourceBuilding;
        }

        private void OnMouseDown()
        {
            OnBuildingClicked?.Invoke(_resourceBuilding);
        }

        public void Dispose()
        {
            OnBuildingClicked = null;
        }
    }
}