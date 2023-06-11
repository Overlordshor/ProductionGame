using System;
using System.Collections.Generic;
using ProductionGame.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IResourceBuildingMenuView : IDisposable
    {
        event Action<ResourceBuildingModel> OnCellClicked;
        event Action<ResourceBuildingModel> OnStartClicked;
        event Action<ResourceBuildingModel> OnStopClicked;

        void Show(ResourceBuildingModel resourceBuilding);
        void SetStartButtonActive(bool active);
    }

    public class ResourceBuildingMenuView : MonoBehaviour, IResourceBuildingMenuView
    {
        public event Action<ResourceBuildingModel> OnCellClicked;
        public event Action<ResourceBuildingModel> OnStartClicked;
        public event Action<ResourceBuildingModel> OnStopClicked;

        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _cellsContainer;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private GameObject _inventoryItemPrefab;

        private List<GameObject> _inventoryItems;


        public void Show(ResourceBuildingModel resourceBuilding)
        {
            ClearInventory();

            foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
            {
                var cellObject = Instantiate(_cellPrefab, _cellsContainer);
                var cellButton = cellObject.GetComponent<Button>();
                var cellText = cellObject.GetComponentInChildren<Text>();
                cellText.text = resource.ToString();

                cellButton.onClick.AddListener(() => OnCellClicked?.Invoke(resourceBuilding));
            }


            _startButton.onClick.AddListener(() => OnStartClicked?.Invoke(resourceBuilding));
            _stopButton.onClick.AddListener(() => OnStopClicked?.Invoke(resourceBuilding));

            gameObject.SetActive(true);
        }

        public void SetStartButtonActive(bool active)
        {
            _startButton.gameObject.SetActive(active);
            _stopButton.gameObject.SetActive(!active);
        }

        private void ClearInventory()
        {
            if (_inventoryItems == null)
                return;

            foreach (var item in _inventoryItems)
                Destroy(item);

            _inventoryItems.Clear();
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _stopButton.onClick.RemoveAllListeners();
        }

        public void Dispose()
        {
            OnCellClicked = null;
            OnStartClicked = null;
            OnStopClicked = null;
        }
    }
}