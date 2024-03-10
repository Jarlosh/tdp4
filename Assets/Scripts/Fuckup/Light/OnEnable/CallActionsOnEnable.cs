using System;
using TDPF.FuckUp.Interactions;
using UnityEngine;
using Utils.GameEvents;

namespace TDPF.FuckUp
{
    public class CallActionsOnEnable: MonoBehaviour, IInteractiveObject
    {
        private readonly GameEvent _onInteractedEvent = new();
        public IGameEvent OnInteractedEvent => _onInteractedEvent;
        
        public bool IsAbleToInteract => false;
        public string Description => throw new Exception();

        private void Awake()
        {
            Interact();
            Debug.Log("call", gameObject);
        }

        public void Interact() => _onInteractedEvent.Invoke();
    }
}