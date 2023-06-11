using System;
using System.Collections.Generic;
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
        [SerializeField] private Text productTypeText;
        [SerializeField] private Dropdown resourceTypeDropdown1;
        [SerializeField] private Dropdown resourceTypeDropdown2;

        [SerializeField] private Button startButton;
        [SerializeField] private Button stopButton;


        public void Show()
        {
            resourceTypeDropdown1.ClearOptions();
            resourceTypeDropdown2.ClearOptions();

            var resourceTypes = Enum.GetValues(typeof(ResourceType));
            var options = new List<Dropdown.OptionData>();

            foreach (var resourceType in resourceTypes)
                options.Add(new Dropdown.OptionData(resourceType.ToString()));

            resourceTypeDropdown1.AddOptions(options);
            resourceTypeDropdown2.AddOptions(options);

            startButton.onClick.AddListener(HandleStartClicked);
            stopButton.onClick.AddListener(HandleStopClicked);
        }

        public void SetStartButtonActive(bool isActive)
        {
            startButton.interactable = isActive;
        }

        public ResourceType GetSelectedResourceType1()
        {
            var selectedOption = resourceTypeDropdown1.options[resourceTypeDropdown1.value];
            return ParseResourceType(selectedOption);
        }

        public ResourceType GetSelectedResourceType2()
        {
            var selectedOption = resourceTypeDropdown2.options[resourceTypeDropdown2.value];
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
            startButton.onClick.RemoveListener(HandleStartClicked);
            stopButton.onClick.RemoveListener(HandleStopClicked);
        }
    }
}