using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(ProductInfo), menuName = "ProductionGame/Product Info", order = 3)]
    public class ProductInfo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private ProductType _productType;
        [SerializeField] private int _price;

        public ProductType ProductType => _productType;
        public string Name => _name;
        public int Price => _price;
    }
}