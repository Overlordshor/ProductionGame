using System;

namespace ProductionGame.GameView
{
    public interface IBuildingView<T> : IDisposable
    {
        void Initialize(T resourceBuilding);

        event Action<T> OnBuildingClicked;
    }
}