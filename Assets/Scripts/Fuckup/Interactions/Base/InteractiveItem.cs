using UnityEngine;
using UnityEngine.Events;
using Utils.GameEvents;

namespace TDPF.FuckUp.Interactions
{
    public interface IInteractiveObject
    {
        string Description { get; }
        IGameEvent OnInteractedEvent { get; }
        bool IsAbleToInteract { get; }
        void Interact();
    }
    
    public class InteractiveItem: FuckUpBehaviour, IInteractiveObject
    {
        [SerializeField] public UnityEvent OnInteracted;

        [SerializeField] private string defaultDescription = "[E] Placeholder Hint";

        public bool IsAbleToInteract { get; set; } = true;
        protected readonly GameEvent onInteractedEvent = new();

        public string Description => defaultDescription;
        public IGameEvent OnInteractedEvent => onInteractedEvent;

        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject);
            ProcessInteract();
            OnInteracted?.Invoke();
            onInteractedEvent?.Invoke();
        }

        protected virtual void ProcessInteract()
        {
            // May be overriden
        }

        protected override void SetState(bool state)
        {
            IsAbleToInteract = state;
        }
    }
}