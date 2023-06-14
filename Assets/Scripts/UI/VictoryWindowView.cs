using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IVictoryWindowView
    {
        event Action OnMainMenuClicked;
        void Show();
    }

    public class VictoryWindowView : MonoBehaviour, IVictoryWindowView, IDisposable
    {
        public event Action OnMainMenuClicked;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private bool _showOnStart;


        private void Start()
        {
            _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
            gameObject.SetActive(_showOnStart);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            OnMainMenuClicked = null;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }


        private void HandleMainMenuClicked()
        {
            Hide();
            OnMainMenuClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _mainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
        }
    }
}