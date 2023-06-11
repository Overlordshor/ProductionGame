using ProductionGame.GameView;
using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(BuildingSettings),
        menuName = "ProductionGame/Resource Building Settings", order = 2)]
    public class BuildingSettings : ScriptableObject
    {
        [SerializeField] private float _productionInterval;
        [SerializeField] private BuildingView _buildingPrefab;
        [SerializeField] private Vector3 _buildingPosition;

        public float ProductionInterval => _productionInterval;
        public BuildingView BuildingPrefab => _buildingPrefab;
        public Vector3 BuildingPosition => _buildingPosition;
    }
}