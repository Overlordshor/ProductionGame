using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "ProductionGame/Building Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private BuildingSettings _marketBuildingSettings;
        [SerializeField] private BuildingSettings _processingBuildingSettings;
        [SerializeField] private BuildingSettings[] _resourceBuildingSettings;

        public BuildingSettings ProcessingBuildingSettings => _processingBuildingSettings;
        public BuildingSettings MarketBuildingSettings => _marketBuildingSettings;
        public BuildingSettings[] ResourceBuildingSettings => _resourceBuildingSettings;
    }
}