using ProductionGame.Infrastructure;
using ProductionGame.Models;
using ProductionGame.UI;

namespace ProductionGame.Controllers
{
    public interface IStorageController
    {
        void Add(ResourceType resourceType);
        void Remove(ResourceType resourceType);
    }

    public class StorageController : IStorageController
    {
        private readonly StorageModel _storageModel;
        private readonly IStorageView _storageView;
        private readonly IGameDataSaver _gameDataSaver;

        public StorageController(StorageModel storageModel, IStorageView storageView, IGameDataSaver gameDataSaver)
        {
            _storageModel = storageModel;
            _storageView = storageView;
            _gameDataSaver = gameDataSaver;
        }

        public void Add(ResourceType resourceType)
        {
            _storageModel.Add(resourceType, 1);
            _storageView.UpdateCount(resourceType, _storageModel.GetCount(resourceType));

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }

        public void Remove(ResourceType resourceType)
        {
            _storageModel.Remove(resourceType);
            _storageView.UpdateCount(resourceType, _storageModel.GetCount(resourceType));

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }
    }
}