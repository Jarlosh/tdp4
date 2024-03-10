using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    [UsedImplicitly]
    public class DialogueSubsystem: FuckUpSubsystemBase
    {
        [Inject] private FuckUpController _fuckUp;
        [Inject] private DialogConfig _config;
        [Inject] private DialogueQueue _dialogueQueue;

        private Dictionary<IActivateKey, List<DialogItem>> _dialogCache;

        public override void Initialize()
        {
            base.Initialize();
            
            _dialogCache = _config.CollectDialoguesByKeys();
            InitUpdate();
        }

        private void InitUpdate()
        {
            foreach (var kp in _dialogCache)
            {
                if (kp.Key.Active)
                {
                    OnActivation(kp.Key);
                }
            }
        }

        protected override void OnActivation(IActivateKey key)
        {
            UpdateDialogues();
            
            void UpdateDialogues()
            {
                if (key == null || !_dialogCache.TryGetValue(key, out var dialogues) || dialogues.Count == 0) 
                    return;
                
                CheckCollection(dialogues, "dialogues");
                
                foreach (var dialogue in Enumerable.Reverse(dialogues))
                {
                    if(dialogue.AllPassing(timeController.CurrentMoment))
                    {
                        _dialogueQueue.Enqueue(dialogue);
                        _dialogueQueue.Enqueue(() => _fuckUp.Activate(dialogue, null));
                    }
                }
            }

            void CheckCollection<T>(List<T> dialogues, string name)
            {
                if (dialogues.Count > 1)
                {
                    var items = string.Join('\n', dialogues.Select(d => d.ToString()));
                    Debug.LogError($"Too many {name}! {dialogues.Count} {items}");
                }
            }
        }
    }
}