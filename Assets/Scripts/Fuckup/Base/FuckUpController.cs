using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TDPF.FuckUp.DialogueSystem;
using UnityEngine;

namespace TDPF.FuckUp
{
    [UsedImplicitly]
    public class FuckUpController
    {
        private HashSet<IActivateKey> EnabledKeys { get; } = new();

        public event Action<IActivateKey> OnKeyChangedEvent;
        
        public void Subscribe(Action<IActivateKey> callback) => OnKeyChangedEvent += callback;

        public void Unsubscribe(Action<IActivateKey> callback) => OnKeyChangedEvent -= callback;
        
        public void Activate(IActivateKey key, GameObject activator)
        {
            if (EnabledKeys.Add(key))
            {
                key.Activate(activator);
                OnKeyChangedEvent?.Invoke(key);
            }
            else
            {
                Debug.LogError($"{key} already active!", activator);
            }
        }

        public void Deactivate(IActivateKey key)
        {
            if (EnabledKeys.Remove(key))
            {
                key.Deactivate();
                OnKeyChangedEvent?.Invoke(key);
            }
            else
            {
                Debug.LogError($"{key} already inactive!");
            }
        }
    }
}