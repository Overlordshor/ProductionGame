using System;
using System.Collections.Generic;
using ProductionGame.GameView;

namespace ProductionGame.Repositories
{
    public interface IBuildingsViewRepository : IDisposable
    {
        void Add<T>(IBuildingView<T> building);

        IReadOnlyCollection<BuildingView> GetAll();

        void Clear();
    }

    public class BuildingsViewRepository : IBuildingsViewRepository
    {
        private List<BuildingView> _buildings = new();

        public void Add<T>(IBuildingView<T> building)
        {
            _buildings.Add(building as BuildingView);
        }

        public IReadOnlyCollection<BuildingView> GetAll()
        {
            return _buildings;
        }

        public void Clear()
        {
            _buildings.Clear();
        }

        public void Dispose()
        {
            _buildings.ForEach(buildingView => buildingView.Dispose());
        }
    }
}