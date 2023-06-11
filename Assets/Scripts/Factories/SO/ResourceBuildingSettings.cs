using ProductionGame.GameView;
using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(ResourceBuildingSettings),
        menuName = "ProductionGame/Resource Building Settings", order = 2)]
    public class ResourceBuildingSettings : ScriptableObject
    {
        [SerializeField] private float _productionInterval;
        [SerializeField] private ResourceBuildingView _resourceBuildingMenuPrefab;
        [SerializeField] private Vector3[] _resourceBuildingPositions;

        public float ProductionInterval => _productionInterval;
        public ResourceBuildingView ResourceBuildingMenuPrefab => _resourceBuildingMenuPrefab;
        public Vector3[] ResourceBuildingPositions => _resourceBuildingPositions;
    }
}