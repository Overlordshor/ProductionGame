using System;
using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.GameView
{
    public interface IResourceBuildingView : IDisposable
    {
        void Initialize(ResourceBuildingModel resourceBuilding);

        event Action<ResourceBuildingModel> OnBuildingClicked;
    }

    public class ResourceBuildingView : MonoBehaviour, IResourceBuildingView
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