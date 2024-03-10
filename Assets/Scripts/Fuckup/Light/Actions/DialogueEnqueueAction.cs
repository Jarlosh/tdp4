using System;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using TDPF.FuckUp.DialogueSystem;
using TDPF.Player;
using UnityEngine;
using Utils;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public enum ActivationStrategy
    {
        JustShow = 1,
        ShowAndActivate = 2,
        ActivateAndShow = 3,
    }


    public class DialogueEnqueueAction: InteractAction
    {
        [Serializable]
        public class EmitterInfo
        {
            public InteractiveObject obj;
            public int playAfter;
        }
        
        [Inject] private PlayerObj _player;
        [Inject] private DialogueQueue _queue;
        [Inject] private FuckUpController _fuckUp;
        [Inject] private SoundsConfig _soundConfig;

        [SerializeField] private bool dontPlayClick = false;
        [SerializeField] private bool fireOnce = true;
        [SerializeField] private ActivationStrategy strategy = ActivationStrategy.ShowAndActivate;
        [SerializeField] private DialogueStack dialogue;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private EmitterInfo[] emitterInfos;
        private bool _used;

        protected override void Awake()
        {
            base.Awake();
            _used = false;
        }

        private void Activate()
        {
            _fuckUp.Activate(dialogue, gameObject);
        }

        private void SetSource(Transform obj)
        {
            _player.DialogueSoundSource = obj;
        }
        
        protected override void Process()
        {
            if (fireOnce && _used)
            {
                return;
            }

            _queue.Enqueue(Start);
            
            if (!dontPlayClick)
            {
                RuntimeManager.PlayOneShot(_soundConfig.StartDialogueSound);
            }
            
            if (!_used && strategy == ActivationStrategy.ActivateAndShow)
            {
                _queue.Enqueue(Activate);
            }

            for (var index = 0; index < dialogue.Items.Count; index++)
            {
                foreach (var info in emitterInfos.Where(e => e.playAfter == index))
                {
                    _queue.Enqueue(() => info.obj.Interact());
                }
                var item = dialogue.Items[index];
                _queue.Enqueue(item);
            }

            if (!_used && strategy == ActivationStrategy.ShowAndActivate)
            {
                _queue.Enqueue(Activate);
            }
            _queue.Enqueue(OnComplete);
            _used = true;

            void Start()
            {
                SetSource(lookTarget);
                _player.InDialogue = true;
            }

            void OnComplete()
            {
                SetSource(null);
                _player.InDialogue = false;
            }
        }
    }
}