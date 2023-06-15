using System;
using ProductionGame.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IResourceBuildingMenuView
    {
        event Action<ResourceBuildingModel, ResourceType> OnNextResourceSelected;
        event Action<ResourceBuildingModel> OnStartClicked;
        void Show(ResourceBuildingModel resourceBuilding);
        void SetStartButtonState(bool active);
        void SetCurrentResource(string resourceTitle, Sprite sprite);
        void ClearCurrentResource();
    }

    public class ResourceBuildingMenuView : View, IResourceBuildingMenuView, IDisposable
    {
        public event Action<ResourceBuildingModel, ResourceType> OnNextResourceSelected;
        public event Action<ResourceBuildingModel> OnStartClicked;

        [SerializeField] private Button _nextResourceButton;
        [SerializeField] private TextMeshProUGUI _resourceText;
        [SerializeField] private Button _startAndStopButton;
        [SerializeField] private TextMeshProUGUI _startAndStopText;
        [SerializeField] private Button _closeButton;
        private Image _resourceImage;

        private ResourceBuildingModel _resourceBuilding;
        private ResourceType[] _resourceTypes;
        private int _currentResourceIndex;
        private Sprite _defaultSprite;


        protected override void OnStart()
        {
            _resourceImage = _nextResourceButton.GetComponent<Image>();
            _defaultSprite = _resourceImage.sprite;
            _startAndStopButton.onClick.AddListener(() => OnStartClicked?.Invoke(_resourceBuilding));
            _nextResourceButton.onClick.AddListener(SelectNextResource);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));

            _resourceTypes = new[]
            {
                ResourceType.Wood,
                ResourceType.Stone,
                ResourceType.Iron
            };
        }

        public void Show(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuilding = resourceBuilding;
            gameObject.SetActive(true);
        }

        public void SetStartButtonState(bool active)
        {
            var text = active
                ? ButtonText.StopText
                : ButtonText.StartText;

            _startAndStopText.SetText(text);
        }

        public void Dispose()
        {
            OnNextResourceSelected = null;
            OnStartClicked = null;
        }

        public void SetCurrentResource(string resourceTitle, Sprite sprite)
        {
            _resourceText.text = resourceTitle;
            _resourceImage.sprite = sprite ?? _defaultSprite;
        }

        public void ClearCurrentResource()
        {
            SetCurrentResource(null, null);
            _currentResourceIndex = 0;
        }

        private void SelectNextResource()
        {
            _currentResourceIndex = (_currentResourceIndex + 1) % _resourceTypes.Length;

            var selectedResource = _resourceTypes[_currentResourceIndex];
            OnNextResourceSelected?.Invoke(_resourceBuilding, selectedResource);
        }

        private void OnDestroy()
        {
            _startAndStopButton.onClick.RemoveAllListeners();
            _nextResourceButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}