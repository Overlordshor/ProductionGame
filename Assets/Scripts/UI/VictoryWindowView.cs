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

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
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

        public void Dispose()
        {
            OnMainMenuClicked = null;
        }
    }
}