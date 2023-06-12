using System;
using System.Collections.Generic;

namespace ProductionGame.Infrasturcture
{
    public interface IDisposables
    {
        void Add(IDisposable disposable);
    }

    public class Disposables : IDisposables, IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
            _disposables.Clear();
        }
    }
}
