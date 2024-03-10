using UnityEngine;
using Utils.GameEvents;

namespace TDPF.FuckUp.Interactions
{
    public class InteractHintController : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI text;

        [SerializeField] private GameObject textBg;

        private InteractiveItemDetector _itemDetector;

        private void Start()
        {
            _itemDetector = FindObjectOfType<InteractiveItemDetector>();
            _itemDetector.onItemHovered.Subscribe(HandleItemHovered);
            _itemDetector.onItemUnhovered.Subscribe(HandleItemUnhovered);

            textBg.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_itemDetector == null)
            {
                return;
            }

            _itemDetector.onItemHovered.Unsubscribe(HandleItemHovered);
            _itemDetector.onItemUnhovered.Unsubscribe(HandleItemUnhovered);
        }

        private void HandleItemHovered(IInteractiveObject item)
        {
            if (!string.IsNullOrEmpty(item.Description))
            {
                textBg.SetActive(true);
                text.text = $"[E] {item.Description}";   
            }
        }

        private void HandleItemUnhovered(IInteractiveObject item)
        {
            text.text = "";
            textBg.SetActive(false);
        }
    }
}