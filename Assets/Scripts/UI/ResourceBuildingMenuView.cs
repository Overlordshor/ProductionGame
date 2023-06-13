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
        event Action<ResourceBuildingModel> OnStopClicked;
        void Show(ResourceBuildingModel resourceBuilding);
        void SetStartButtonState(bool active);
        void SetCurrentResource(string resourceTitle, Sprite sprite);
        void ClearCurrentResource();
    }

    public class ResourceBuildingMenuView : MonoBehaviour, IResourceBuildingMenuView, IDisposable
    {
        private static readonly string StartText = "Start";
        private static readonly string StopText = "Stop";

        public event Action<ResourceBuildingModel, ResourceType> OnNextResourceSelected;
        public event Action<ResourceBuildingModel> OnStartClicked;
        public event Action<ResourceBuildingModel> OnStopClicked;

        [SerializeField] private Button _nextResourceButton;
        [SerializeField] private TextMeshProUGUI _resourceText;
        [SerializeField] private bool _showOnStart;
        [SerializeField] private Button _startAndStopButton;
        [SerializeField] private TextMeshProUGUI _startAndStopText;
        [SerializeField] private Button _closeButton;
        private Image _resourceImage;

        private ResourceBuildingModel _resourceBuilding;
        private ResourceType[] _resourceTypes;
        private int _currentResourceIndex;


        private void Start()
        {
            _resourceImage = _nextResourceButton.GetComponent<Image>();
            _startAndStopButton.onClick.AddListener(() =>
            {
                if (_resourceBuilding != null)
                    OnStartClicked?.Invoke(_resourceBuilding);
            });
            _nextResourceButton.onClick.AddListener(SelectNextResource);
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));


            gameObject.SetActive(_showOnStart);
        }

        public void Show(ResourceBuildingModel resourceBuilding)
        {
            _resourceBuilding = resourceBuilding;
            _resourceTypes = new[]
            {
                ResourceType.Wood,
                ResourceType.Stone,
                ResourceType.Iron
            };

            gameObject.SetActive(true);
        }

        public void SetStartButtonState(bool active)
        {
            var text = active
                ? StopText
                : StartText;

            _startAndStopText.SetText(text);
        }

        public void Dispose()
        {
            OnNextResourceSelected = null;
            OnStartClicked = null;
            OnStopClicked = null;
        }

        public void SetCurrentResource(string resourceTitle, Sprite sprite)
        {
            _resourceText.text = resourceTitle;
            _resourceImage.sprite = sprite;
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
        }
    }
}