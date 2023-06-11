using System;
using System.Collections.Generic;
using ProductionGame.GameView;
using ProductionGame.Models;

namespace ProductionGame.Repositories
{
    public interface IBuildingsViewRepository
    {
        void Add<T>(IBuildingView<T> building);
    }

    public class BuildingsViewRepository : IBuildingsViewRepository, IDisposable
    {
        private List<BuildingView> _resourceBuildings = new();

        public void Add<T>(IBuildingView<T> building)
        {
            if (typeof(T) == typeof(ResourceBuildingModel))
                _resourceBuildings.Add(building as BuildingView);
        }

        public void Dispose()
        {
            _resourceBuildings.ForEach(buildingView => buildingView.Dispose());
        }
    }
}