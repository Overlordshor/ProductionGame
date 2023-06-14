using System.Collections.Generic;
using ProductionGame.Infrastructure;
using ProductionGame.Models;
using ProductionGame.SO;
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
        private readonly IGameDataSaver _gameDataSaver;
        private readonly Dictionary<ResourceType, ResourcesInfo> _resourcesInfo;
        private readonly StorageModel _storageModel;
        private readonly IStorageView _storageView;

        public StorageController(StorageModel storageModel,
            IStorageView storageView,
            IGameDataSaver gameDataSaver,
            Dictionary<ResourceType, ResourcesInfo> resourcesInfo)
        {
            _storageModel = storageModel;
            _storageView = storageView;
            _gameDataSaver = gameDataSaver;
            _resourcesInfo = resourcesInfo;
        }

        public void Add(ResourceType resourceType)
        {
            _storageModel.Add(resourceType, 1);
            _storageView.UpdateCount(_resourcesInfo[resourceType], _storageModel.GetCount(resourceType));

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }

        public void Remove(ResourceType resourceType)
        {
            _storageModel.Remove(resourceType);
            _storageView.UpdateCount(_resourcesInfo[resourceType], _storageModel.GetCount(resourceType));

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }
    }
}