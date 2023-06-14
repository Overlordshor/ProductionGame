using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProductionGame.UI
{
    public class ResourceStorageItemView : MonoBehaviour
    {
        [SerializeField] private Image _resourceImage;
        [SerializeField] private TextMeshProUGUI _resourceCountText;

        public void SetSprite(Sprite sprite)
        {
            _resourceImage.sprite = sprite;
        }

        public void SetCount(int count)
        {
            _resourceCountText.text = count.ToString();
        }
    }
}