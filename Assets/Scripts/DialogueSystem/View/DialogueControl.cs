using System;
using System.Linq;
using TDPF.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.GameEvents;
using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    public class DialogueController : MonoBehaviour
    {
        [Inject] private DialogueQueue _queue;

        [SerializeField] public DialogueView dialogueView;
        [SerializeField] public InputActionReference closeAction;
        [SerializeField] public InputActionReference moveInput;
        [SerializeField] public InputActionReference rotationInput;

        private IDialogueItem _current;
        private IDialogueProcessor _processor;
        public bool IsPlaying => _current != null; 
        public bool HasProcessor => _processor != null; 
        
        private void Awake()
        {
            closeAction.action.SetPerformSubscription(true, TrySkip);
            _queue.OnEnqueue.Subscribe(OnEnqueue);
        }

        private void OnDestroy()
        {
            closeAction.action.SetPerformSubscription(false, TrySkip);
            _queue.OnEnqueue.Unsubscribe(OnEnqueue);
        }

        private void TrySkip(InputAction.CallbackContext obj) => Skip();

        private void OnEnqueue(IDialogueNode node)
        {
            TryStart();
        }

        private void TryStart()
        {
            if (!IsPlaying && !HasProcessor && _queue.Any)
            {
                _queue.Dequeue().Process(this);
            }
        }

        public void Complete()
        {
            dialogueView.ClearData();
            _current = null;
            _processor = null;

            if (!_queue.Any)
            {
                SetState(false);
            }
            else
            {
                _processor = _queue.Dequeue();
                _processor.Process(this);
            }
        }

        private void SetState(bool state)
        {
            if (state)
            {
                closeAction.action.Enable();
                moveInput.action.Disable();
                rotationInput.action.Disable();
                dialogueView.ShowOrUpdate(_current);
            }
            else
            {
                closeAction.action.Disable();
                moveInput.action.Enable();
                rotationInput.action.Enable();
                dialogueView.Hide();
            }
        }

        private void Skip()
        {
            if(IsPlaying)
            {
                Complete();
            }
        }

        public void Play(IDialogueItem item)
        {
            _current = item;
            SetState(true);
        }
    }
}