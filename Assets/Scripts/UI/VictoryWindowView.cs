using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IVictoryWindowView
    {
        event Action OnMainMenuClicked;
        void Show();
        void SetCurrentResult(int coins);
    }

    public class VictoryWindowView : View, IVictoryWindowView, IDisposable
    {
        public event Action OnMainMenuClicked;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private RectTransform _popup;
        [SerializeField] private float _animationDuration = 0.5f;

        protected override void OnStart()
        {
            _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
        }

        public void Show()
        {
            _popup.anchoredPosition = new Vector3(0f, -Screen.height, 0f);
            gameObject.SetActive(true);
            _popup.DOKill();
            _popup.DOLocalMoveY(0f, _animationDuration)
                .SetEase(Ease.OutBack);
        }

        public void SetCurrentResult(int coins)
        {
            _winText.text = $"You won by collecting {coins} coins";
        }

        public void Dispose()
        {
            OnMainMenuClicked = null;
        }

        private void Hide()
        {
            _popup.DOKill();
            _popup.anchoredPosition = new Vector3(0f, 0, 0f);
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