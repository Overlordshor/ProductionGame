using System;
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

    public class StorageController : IStorageController, IDisposable
    {
        private readonly IGameDataSaver _gameDataSaver;
        private readonly Dictionary<ResourceType, ResourcesInfo> _resourcesInfo;
        private readonly PlayerModel _playerModel;
        private readonly StorageModel _storageModel;
        private readonly IStorageView _storageView;

        public StorageController(PlayerModel playerModel,
            StorageModel storageModel,
            IStorageView storageView,
            IGameDataSaver gameDataSaver,
            Dictionary<ResourceType, ResourcesInfo> resourcesInfo)
        {
            _playerModel = playerModel;
            _storageModel = storageModel;
            _storageView = storageView;
            _gameDataSaver = gameDataSaver;
            _resourcesInfo = resourcesInfo;

            _playerModel.OnCoinsChanged += _storageView.UpdateCoinCount;
            _storageModel.OnResourcesChanged += UpdateResourcesCount;
        }

        public void ApplyResource()
        {
            _storageView.UpdateCoinCount(_playerModel.Coins);
            _storageView.Clear();
            foreach (var availableResource in _storageModel.GetAvailableResources())
                UpdateResourcesCount(availableResource);
        }

        public void Add(ResourceType resourceType)
        {
            _storageModel.Add(resourceType, 1);

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }

        public void Remove(ResourceType resourceType)
        {
            _storageModel.Remove(resourceType);

            _gameDataSaver.Change(resourceType, _storageModel.GetCount(resourceType));
            _gameDataSaver.SaveChanges();
        }

        private void UpdateResourcesCount(ResourceType resourceType)
        {
            _storageView.UpdateCount(_resourcesInfo[resourceType], _storageModel.GetCount(resourceType));
        }

        public void Dispose()
        {
            _playerModel.OnCoinsChanged -= _storageView.UpdateCoinCount;
        }
    }
}