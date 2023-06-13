using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.SO
{
    [CreateAssetMenu(fileName = nameof(ResourcesInfo), menuName = "ProductionGame/Resource Info", order = 3)]
    public class ResourcesInfo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _price;

        public ResourceType ResourceType => _resourceType;
        public string Name => _name;
        public int Price => _price;
        public Sprite Sprite => _sprite;
    }
}