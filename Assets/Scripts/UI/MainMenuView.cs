using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IMainMenuView : IDisposable
    {
        event Action<int> OnBuildCountSelected;
        event Action OnStartGameClicked;

        void Show();
    }

    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public event Action<int> OnBuildCountSelected;
        public event Action OnStartGameClicked;

        [SerializeField] private Toggle[] _buildingCountToggles;
        [SerializeField] private Button _startButton;

        private void Start()
        {
            for (var index = 0; index < _buildingCountToggles.Length; index++)
            {
                var buildingCountToggle = _buildingCountToggles[index];
                var buildingCount = index + 1;
                buildingCountToggle.onValueChanged.AddListener(value =>
                {
                    if (value)
                        OnBuildCountSelected?.Invoke(buildingCount);
                });
            }

            _startButton.onClick.AddListener(() =>
            {
                OnStartGameClicked?.Invoke();
                gameObject.SetActive(false);
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            OnBuildCountSelected = null;
            OnStartGameClicked = null;
        }

        private void OnDestroy()
        {
            foreach (var buildingCountToggle in _buildingCountToggles)
                buildingCountToggle.onValueChanged.RemoveAllListeners();

            Dispose();
        }
    }
}
