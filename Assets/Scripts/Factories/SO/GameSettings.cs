using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "ProductionGame/Game Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private BuildingSettings _marketBuildingSettings;
        [SerializeField] private BuildingSettings _processingBuildingSettings;
        [SerializeField] private BuildingSettings[] _resourceBuildingSettings;
        [SerializeField] private ProductInfo[] _productsInfo;
        [SerializeField] private int _coinsToWin;

        public BuildingSettings ProcessingBuildingSettings => _processingBuildingSettings;
        public BuildingSettings MarketBuildingSettings => _marketBuildingSettings;
        public BuildingSettings[] ResourceBuildingSettings => _resourceBuildingSettings;
        public ProductInfo[] ProductsInfo => _productsInfo;
        public int CoinsToWin => _coinsToWin;
    }
}