using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IVictoryWindowView
    {
        event Action OnMainMenuClicked;
        void Show();
    }

    public class VictoryWindowView : View, IVictoryWindowView, IDisposable
    {
        public event Action OnMainMenuClicked;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private RectTransform _popup;
        [SerializeField] private float _animationDuration = 0.5f;

        protected override void OnStart()
        {
            _popup.position = new Vector3(0f, -Screen.height, 0f);
            _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _popup.DOKill();
            _popup
                .DOLocalMoveY(0f, _animationDuration)
                .SetEase(Ease.OutBack);
        }

        public void Dispose()
        {
            OnMainMenuClicked = null;
        }

        private void Hide()
        {
            _popup.DOKill();
            _popup.DOLocalMoveY(-Screen.height, _animationDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
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