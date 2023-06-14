using System.Linq;

namespace ProductionGame.Models
{
    public class Craft
    {
        public ResourceType[] Resources { get; }
        public ResourceType Product { get; }

        public Craft(ResourceType[] resources)
        {
            Resources = resources;
            Product = CraftProduct(resources.First(), resources.Last());
        }

        private ResourceType CraftProduct(ResourceType resource1Type, ResourceType resource2Type)
        {
            return resource1Type switch
            {
                ResourceType.Wood when resource2Type == ResourceType.Stone => ResourceType.Hammers,
                ResourceType.Wood when resource2Type == ResourceType.Iron => ResourceType.Forks,
                ResourceType.Stone when resource2Type == ResourceType.Iron => ResourceType.Drills,
                ResourceType.Stone when resource2Type == ResourceType.Wood => ResourceType.Hammers,
                ResourceType.Iron when resource2Type == ResourceType.Wood => ResourceType.Forks,
                ResourceType.Iron when resource2Type == ResourceType.Stone => ResourceType.Drills,
                _ => ResourceType.None
            };
        }
    }
}