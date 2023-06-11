using ProductionGame.GameView;
using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = "BuildingPrefabs", menuName = "ProductionGame/Building Prefabs", order = 1)]
    public class BuildingPrefabs : ScriptableObject
    {
        [SerializeField] private Vector3 _marketPosition;
        [SerializeField] private GameObject _marketPrefab;
        [SerializeField] private Vector3 _processingBuildingPosition;
        [SerializeField] private GameObject _processingBuildingPrefab;
        [SerializeField] private ResourceBuildingView _resourceBuildingMenuPrefab;

        [SerializeField] private Vector3[] _resourceBuildingPositions;

        public ResourceBuildingView ResourceBuildingMenuPrefab => _resourceBuildingMenuPrefab;
        public GameObject ProcessingBuildingPrefab => _processingBuildingPrefab;
        public GameObject MarketPrefab => _marketPrefab;

        public Vector3[] ResourceBuildingPositions => _resourceBuildingPositions;
        public Vector3 ProcessingBuildingPosition => _processingBuildingPosition;
        public Vector3 MarketPosition => _marketPosition;
    }
}