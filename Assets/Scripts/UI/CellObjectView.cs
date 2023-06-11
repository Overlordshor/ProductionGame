using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    [RequireComponent(typeof(Button))]
    public class CellObjectView : MonoBehaviour
    {
        private Button _cellButton;
        private TextMeshProUGUI _cellText;

        private void Start()
        {
            _cellButton = GetComponent<Button>();
            _cellText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            _cellText.SetText(text);
        }

        public void Subscribe(Action action)
        {
            _cellButton.onClick.AddListener(() => action?.Invoke());
        }

        private void OnDestroy()
        {
            _cellButton.onClick.RemoveAllListeners();
        }
    }
}