using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IStorageController
    {
        void AddResource(ResourceType resourceType);
    }

    public class StorageController : IStorageController
    {
        private StorageModel _storageModel;
        private IStorageView _storageView;

        public StorageController(StorageModel storageModel, IStorageView storageView)
        {
            _storageModel = storageModel;
            _storageView = storageView;
        }

        public void AddResource(ResourceType resourceType)
        {
            _storageModel.AddResource(resourceType, 1);
            _storageView.UpdateResourceCount(resourceType, _storageModel.GetResourceCount(resourceType));
        }
    }
}