using TDPF.TimeSwitch;
using UnityEngine;
using Utils.GameEvents;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public abstract class InteractAction : MonoBehaviour
    {
        [Inject] protected TimeSwitcher timeController;
        
        private IInteractiveObject _item;

        protected virtual void Awake()
        {
            _item = GetComponent<IInteractiveObject>();
            if (_item == null)
            {
                Debug.LogError("No IInteractiveObject!", gameObject);
            }
        }

        protected virtual void OnEnable()
        {
            _item.OnInteractedEvent.Subscribe(Process);
        }

        protected virtual void OnDisable()
        {
            _item.OnInteractedEvent.Unsubscribe(Process);
        }

        protected abstract void Process();
    }
}