using System;
using UnityEngine;

namespace ProductionGame.GameView
{
    public abstract class BuildingView : MonoBehaviour, IDisposable
    {
        protected abstract void Click();
        public abstract void Dispose();
    }
}