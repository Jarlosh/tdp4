using UnityEngine;
using Utils.GameEvents;

namespace TDPF.FuckUp.Interactions
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class InteractiveObject: MonoBehaviour, IInteractiveObject
    {
        [SerializeField] protected string defaultDescription = "Placeholder Hint";
        [SerializeField] private RestrictionsHolder optionalRestrictions;

        protected readonly GameEvent onInteractedEvent = new();
        public IGameEvent OnInteractedEvent => onInteractedEvent;
        
        public virtual string Description => defaultDescription;
        public virtual bool IsAbleToInteract => gameObject.activeInHierarchy && AllPassing;
        public bool AllPassing => (optionalRestrictions == null || optionalRestrictions.AllPassing);
        
        public void Interact()
        {
            if(IsAbleToInteract)
            {
                ProcessInteract();
                onInteractedEvent?.Invoke();
            }
        }

        protected virtual void ProcessInteract()
        {
        }
    }
}