using TDPF.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Utils.GameEvents;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public class InteractiveItemDetector : MonoBehaviour
    {
        [Inject] private PlayerObj _player;
        public readonly GameEvent<IInteractiveObject> onItemHovered = new();
        public readonly GameEvent<IInteractiveObject> onItemUnhovered = new();

        [SerializeReference] private Transform cameraTransform;
        
        [SerializeField]
        private LayerMask interactiveLayer;
        
        [SerializeField]
        float maxRaycastDistance = 2;
        
        [SerializeField] 
        InputActionReference interactInput;

        private void Update()
        {
            if (!_player.InDialogue && Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out var hitInfo,
                    maxRaycastDistance, interactiveLayer))
            {
                var hitInteractiveItem = hitInfo.collider.GetComponent<IInteractiveObject>();
                if (hitInteractiveItem == null)
                {
                    TryUnhoverCurrentItem();
                    return;
                }

                if (hitInteractiveItem == hoveredItem)
                {
                    if (!hitInteractiveItem.IsAbleToInteract)
                    {
                        TryUnhoverCurrentItem();
                    }

                    return;
                }
                else if (!hitInteractiveItem.IsAbleToInteract)
                {
                    return;
                }

                TryUnhoverCurrentItem();
                HoverItem(hitInteractiveItem);
            }
            else
            {
                TryUnhoverCurrentItem();
            }
        }

        private IInteractiveObject hoveredItem;

        private void Awake()
        {
            interactInput.action.performed += (_) => TryInteractiWithHoveredItem();
            interactInput.action.Enable();
        }

        private void HoverItem(IInteractiveObject hoveredItem)
        {
            this.hoveredItem = hoveredItem;
            onItemHovered?.Invoke(hoveredItem);
        }

        private void TryUnhoverCurrentItem()
        {
            if (hoveredItem == null)
            {
                return;
            }

            onItemUnhovered?.Invoke(hoveredItem);
            hoveredItem = null;
        }

        private void TryInteractiWithHoveredItem()
        {
            hoveredItem?.Interact();
        }
    }
}