using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabs", menuName = "ProductionGame/Building Prefabs", order = 1)]
    public class BuildingPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject _resourceBuildingPrefab;
        [SerializeField] private GameObject _processingBuildingPrefab;
        [SerializeField] private GameObject _marketPrefab;

        public GameObject ResourceBuildingPrefab => _resourceBuildingPrefab;
        public GameObject ProcessingBuildingPrefab => _processingBuildingPrefab;
        public GameObject MarketPrefab => _marketPrefab;
    }
}