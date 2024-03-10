using System;
using FMODUnity;
using TDPF.FuckUp;
using TDPF.FuckUp.DialogueSystem;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class SoundsConfig
    {
        [SerializeField] private ClueKey inventoryKey;
        public IActivateKey InventoryKey => inventoryKey;
        
        [field: SerializeField] public EventReference StartDialogueSound { get; set; }
        [field: SerializeField] public EventReference TakeSound { get; set; }
        [field: SerializeField] public EventReference ActivateSound { get; set; }
        [field: SerializeField] public EventReference FailTakeSound { get; set; }
        [field: SerializeField] public EventReference FailActivateSound { get; set; }

        public void Play(EventReference sound) => RuntimeManager.PlayOneShot(sound);
        public void Play(EventReference sound, GameObject go) => RuntimeManager.PlayOneShotAttached(sound, go);
    }
}