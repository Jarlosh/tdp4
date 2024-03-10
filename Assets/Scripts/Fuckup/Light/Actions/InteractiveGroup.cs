using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.GameEvents;

namespace TDPF.FuckUp.Interactions
{
    public class InteractiveGroup: MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private List<InteractiveObject> group;

        public string Description => GetFirstOrDefault()?.Description ?? "N/A";

        private readonly GameEvent _onInteractedEvent = new();
        public IGameEvent OnInteractedEvent => _onInteractedEvent;

        public bool IsAbleToInteract => (GetFirstOrDefault()?.IsAbleToInteract ?? false) && gameObject.activeInHierarchy;

        private IInteractiveObject GetFirstOrDefault()
        {
            return group.FirstOrDefault(i => i.IsAbleToInteract);
        }
        
        public void Interact()
        {
            foreach (var interactiveObject in group.Where(i => i.IsAbleToInteract))
            {
                interactiveObject.Interact();
            }
            _onInteractedEvent.Invoke();
        }
    }
}