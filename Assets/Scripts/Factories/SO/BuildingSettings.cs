using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(BuildingSettings), menuName = "ProductionGame/Building Settings", order = 1)]
    public class BuildingSettings : ScriptableObject
    {
        [SerializeField] private Vector3 _marketPosition;
        [SerializeField] private GameObject _marketPrefab;
        [SerializeField] private Vector3 _processingBuildingPosition;
        [SerializeField] private GameObject _processingBuildingPrefab;
        [SerializeField] private ResourceBuildingSettings _resourceBuildingSettings;

        public GameObject ProcessingBuildingPrefab => _processingBuildingPrefab;
        public GameObject MarketPrefab => _marketPrefab;
        public Vector3 ProcessingBuildingPosition => _processingBuildingPosition;
        public Vector3 MarketPosition => _marketPosition;
        public ResourceBuildingSettings ResourceBuildingSettings => _resourceBuildingSettings;
    }
}