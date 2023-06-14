using System;
using System.Linq;
using ProductionGame.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IProcessingBuildingMenuView
    {
        event Action<ResourceType, ResourceType> OnStartClicked;
        event Action<ResourceType, ResourceType> OnResourcesSelected;
        void Show();
        void SetStartButtonState(bool isActive);
        void SetCurrentCraftView(Sprite[] resourceSprites, Sprite productSprite);
        void SetActiveStartButton(bool isEnable);
    }

    public class ProcessingBuildingMenuView : MonoBehaviour, IProcessingBuildingMenuView, IDisposable
    {
        public event Action<ResourceType, ResourceType> OnStartClicked;
        public event Action<ResourceType, ResourceType> OnResourcesSelected;

        [SerializeField] private bool _showOnStart;
        [SerializeField] private Button _resource1Button;
        [SerializeField] private Button _resource2Button;
        [SerializeField] private TextMeshProUGUI _productText;
        [SerializeField] private Button _startAndStopButton;
        [SerializeField] private TextMeshProUGUI _startAndStopText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _productImage;
        private Image _resource1Image;
        private Image _resource2Image;

        private bool isStart;
        private ResourceType[] _resourceTypes;

        private int[] _currentResourceIndexes = { 0, 0 };

        private void Start()
        {
            _resource1Image = _resource1Button.GetComponent<Image>();
            _resource2Image = _resource2Button.GetComponent<Image>();

            _resource1Button.onClick.AddListener(SelectFirstResource);
            _resource2Button.onClick.AddListener(SelectSecondResource);
            _startAndStopButton.onClick.AddListener(() =>
            {
                var resource1 = _resourceTypes[_currentResourceIndexes[0]];
                var resource2 = _resourceTypes[_currentResourceIndexes[1]];
                OnStartClicked?.Invoke(resource1, resource2);
            });

            _resourceTypes = new[]
            {
                ResourceType.None,
                ResourceType.Wood,
                ResourceType.Stone,
                ResourceType.Iron
            };

            gameObject.SetActive(_showOnStart);
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetStartButtonState(bool isActive)
        {
            var text = isActive
                ? ButtonText.StopText
                : ButtonText.StartText;

            _startAndStopText.SetText(text);
        }

        public void SetCurrentCraftView(Sprite[] resourceSprites, Sprite productSprite)
        {
            _resource1Image.sprite = resourceSprites.First();
            _resource2Image.sprite = resourceSprites.Last();
            _productImage.sprite = productSprite;
        }

        public void SetActiveStartButton(bool isEnable)
        {
            _startAndStopButton.interactable = isEnable;
        }

        public void Dispose()
        {
            OnResourcesSelected = null;
            OnStartClicked = null;
        }

        private void SelectSecondResource()
        {
            _currentResourceIndexes[1] = (_currentResourceIndexes[1] + 1) % _resourceTypes.Length;
            SelectResources();
        }

        private void SelectFirstResource()
        {
            _currentResourceIndexes[0] = (_currentResourceIndexes[0] + 1) % _resourceTypes.Length;
            SelectResources();
        }

        private void SelectResources()
        {
            var resource1 = _resourceTypes[_currentResourceIndexes[0]];
            var resource2 = _resourceTypes[_currentResourceIndexes[1]];
            OnResourcesSelected?.Invoke(resource1, resource2);
        }


        private void OnDestroy()
        {
            _startAndStopButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}