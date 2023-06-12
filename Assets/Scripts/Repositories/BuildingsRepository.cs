using System;
using System.Collections.Generic;
using ProductionGame.GameView;

namespace ProductionGame.Repositories
{
    public interface IBuildingsViewRepository
    {
        void Add<T>(IBuildingView<T> building);

        IReadOnlyCollection<BuildingView> GetAll();
    }

    public class BuildingsViewRepository : IBuildingsViewRepository, IDisposable
    {
        private List<BuildingView> _buildings = new();

        public void Add<T>(IBuildingView<T> building)
        {
            _buildings.Add(building as BuildingView);
        }

        public IReadOnlyCollection<BuildingView> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _buildings.ForEach(buildingView => buildingView.Dispose());
        }
    }
}