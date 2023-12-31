using System.Collections.Generic;
using Newtonsoft.Json;
using ProductionGame.Infrastructure.Data;
using ProductionGame.Models;
using UnityEngine;

namespace ProductionGame.Infrastructure
{
    public interface IGameDataSaver
    {
        GameData Load();
        public void ChangeCoins(int coins);
        void Change(ResourceType resourceType, int count);
        void SaveChanges();
        void Reset();
    }

    public class GameDataSaver : IGameDataSaver
    {
        private GameData _currentGameData;

        public GameData Load()
        {
            var json = PlayerPrefs.GetString(nameof(GameData));
            _currentGameData = !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<GameData>(json)
                : new GameData(0, new Dictionary<ResourceType, int>(0));

            return _currentGameData;
        }

        public void ChangeCoins(int coins)
        {
            _currentGameData.SetCoinsCount(coins);
        }

        public void Change(ResourceType resourceType, int count)
        {
            _currentGameData.SetResourceCount(new KeyValuePair<ResourceType, int>(resourceType, count));
        }


        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_currentGameData);
            PlayerPrefs.SetString(nameof(GameData), json);
            PlayerPrefs.Save();
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}