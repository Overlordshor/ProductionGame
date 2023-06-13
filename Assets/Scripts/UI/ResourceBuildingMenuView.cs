using System;
using System.Collections.Generic;
using ProductionGame.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public interface IResourceBuildingMenuView : IDisposable
    {
        event Action<ResourceBuildingModel, ResourceType> OnCellClicked;
        event Action<ResourceBuildingModel> OnStartClicked;
        event Action<ResourceBuildingModel> OnStopClicked;

        void Show(ResourceBuildingModel resourceBuilding);
        void SetStartButtonActive(bool active);
    }

    public class ResourceBuildingMenuView : MonoBehaviour, IResourceBuildingMenuView
    {
        public event Action<ResourceBuildingModel, ResourceType> OnCellClicked;
        public event Action<ResourceBuildingModel> OnStartClicked;
        public event Action<ResourceBuildingModel> OnStopClicked;

        [SerializeField] private CellObjectView _cellPrefab;
        [SerializeField] private Transform _cellsContainer;
        [SerializeField] private GameObject _inventoryItemPrefab;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private bool showOnStart;


        private List<GameObject> _inventoryItems;

        private void Start()
        {
            gameObject.SetActive(showOnStart);
        }

        public void Show(ResourceBuildingModel resourceBuilding)
        {
            ClearInventory();

            foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
            {
                var cellObject = Instantiate(_cellPrefab, _cellsContainer);
                cellObject.SetText(resource.ToString());
                cellObject.Subscribe(() => OnCellClicked?.Invoke(resourceBuilding, resource));
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

        public void Dispose()
        {
            OnCellClicked = null;
            OnStartClicked = null;
            OnStopClicked = null;
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
    }
}