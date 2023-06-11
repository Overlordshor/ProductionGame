using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IStorageController
    {
        void Add(ResourceType resourceType);

        void Add(ProductType productType);
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

        public void Add(ResourceType resourceType)
        {
            _storageModel.Add(resourceType, 1);
            _storageView.UpdateCount(resourceType, _storageModel.GetCount(resourceType));
        }

        public void Add(ProductType productType)
        {
            _storageModel.Add(productType, 1);
            _storageView.UpdateCount(productType, _storageModel.GetCount(productType));
        }
    }
}