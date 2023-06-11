using UnityEngine;

namespace ProductionGame.Factories
{
    public abstract class ViewFactory
    {
        protected GameObject InstantiatePrefab(GameObject prefab)
        {
            return Object.Instantiate(prefab);
        }
    }
}