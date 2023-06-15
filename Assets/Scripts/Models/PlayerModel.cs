using System;

namespace ProductionGame.Models
{
    public class PlayerModel : IDisposable
    {
        public event Action<int> OnCoinsChanged;
        public int Coins { get; private set; }

        public PlayerModel(int initCoinsCount)
        {
            Coins = initCoinsCount;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void SubtractCoins(int amount)
        {
            Coins -= amount;
            OnCoinsChanged?.Invoke(Coins);
        }

        public void Dispose()
        {
            OnCoinsChanged = null;
        }
    }
}