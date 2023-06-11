using ProductionGame.GameView;
using UnityEngine;

namespace ProductionGame.Factories
{
    public abstract class ViewFactory
    {
        protected IBuildingView<T> InstantiateBuildingView<T>(BuildingView prefab, Vector3 position)
        {
            return (IBuildingView<T>)Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}