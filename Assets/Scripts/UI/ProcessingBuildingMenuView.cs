using System;
using System.Linq;
using ProductionGame.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IProcessingBuildingMenuView
    {
        event Action OnStartClicked;
        event Action OnStopClicked;

        void Show();
        void SetStartButtonActive(bool isActive);
        ResourceType GetSelectedResourceType1();
        ResourceType GetSelectedResourceType2();
    }

    public class ProcessingBuildingMenuView : MonoBehaviour, IProcessingBuildingMenuView, IDisposable
    {
        public event Action OnStartClicked;
        public event Action OnStopClicked;
        [SerializeField] private Text _resourceTypeText;
        [SerializeField] private Dropdown _resourceTypeDropdown1;
        [SerializeField] private Dropdown _resourceTypeDropdown2;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private bool _showOnStart;

        private void Start()
        {
            gameObject.SetActive(_showOnStart);
        }
        public void Show()
        {
            _resourceTypeDropdown1.ClearOptions();
            _resourceTypeDropdown2.ClearOptions();

            var resourceTypes = new[]
            {
                ResourceType.None,
                ResourceType.Wood,
                ResourceType.Stone,
                ResourceType.Iron
            };

            var options = resourceTypes
                .Select(resourceType => new Dropdown.OptionData(resourceType.ToString()))
                .ToList();

            _resourceTypeDropdown1.AddOptions(options);
            _resourceTypeDropdown2.AddOptions(options);

            _startButton.onClick.AddListener(HandleStartClicked);
            _stopButton.onClick.AddListener(HandleStopClicked);
        }

        public void SetStartButtonActive(bool isActive)
        {
            _startButton.interactable = isActive;
        }

        public ResourceType GetSelectedResourceType1()
        {
            var selectedOption = _resourceTypeDropdown1.options[_resourceTypeDropdown1.value];
            return ParseResourceType(selectedOption);
        }

        public ResourceType GetSelectedResourceType2()
        {
            var selectedOption = _resourceTypeDropdown2.options[_resourceTypeDropdown2.value];
            return ParseResourceType(selectedOption);
        }

        public void Dispose()
        {
            OnStartClicked = null;
            OnStopClicked = null;
        }

        private ResourceType ParseResourceType(Dropdown.OptionData selectedOption)
        {
            return (ResourceType)Enum.Parse(typeof(ResourceType), selectedOption.text);
        }

        private void HandleStartClicked()
        {
            OnStartClicked?.Invoke();
        }

        private void HandleStopClicked()
        {
            OnStopClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(HandleStartClicked);
            _stopButton.onClick.RemoveListener(HandleStopClicked);
        }
    }
}