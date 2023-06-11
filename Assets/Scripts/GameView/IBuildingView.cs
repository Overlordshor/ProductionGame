using System;

namespace ProductionGame.GameView
{
    public interface IBuildingView<T> : IDisposable
    {
        void Initialize(T model);

        event Action<T> OnBuildingClicked;
    }
}