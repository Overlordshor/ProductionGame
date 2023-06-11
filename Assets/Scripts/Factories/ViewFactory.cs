using UnityEngine;

namespace ProductionGame.Factories
{
    public abstract class ViewFactory
    {
        protected T InstantiatePrefab<T>(T prefab, Vector3 position) where T : Component
        {
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        protected GameObject InstantiatePrefab(GameObject prefab, Vector3 position)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}