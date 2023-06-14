using UnityEngine;

namespace ProductionGame.UI
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] private Collider _blockInput;
        [SerializeField] private bool _showOnStart;

        private bool isFirstStart = true;

        private void Start()
        {
            OnStart();
            gameObject.SetActive(_showOnStart);
            isFirstStart = false;
        }

        protected virtual void OnStart()
        {
        }

        private void OnEnable()
        {
            if (!isFirstStart)
                _blockInput.enabled = true;
        }

        private void OnDisable()
        {
            if (_blockInput == null)
                return;

            if (!isFirstStart)
                _blockInput.enabled = false;
        }
    }
}