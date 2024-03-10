using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TDPF.FuckUp.DialogueSystem;
using TDPF.TimeSwitch;
using TDPF.Tools;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TDPF.FuckUp
{
    public abstract class FuckUpBehaviour : MonoBehaviour, IWithClueRestriction
    {
        [Inject] protected FuckUpController fuckUpController;
        [Inject] protected TimeSwitcher timeController;

        [field: SerializeField] public TimeMoment TimeMoment { get; private set; } = TimeMoment.Future | TimeMoment.Past;
        [SerializeField] protected List<ClueKey> keys;
        [SerializeField] protected List<ClueKey> invertedKeys;
        IList<ClueKey> IWithClueRestriction.Keys => keys;
        IList<ClueKey> IWithClueRestriction.NotKeys => invertedKeys;

        public void Start()
        {
            fuckUpController.Subscribe(OnClueActivate);
            OnClueActivate(null);
        }

        private void OnDestroy()
        {
            fuckUpController.Unsubscribe(OnClueActivate);
        }

        protected virtual void OnClueActivate([CanBeNull] IActivateKey changed)
        {
            SetState(this.AllPassing(timeController.CurrentMoment));
        }

        protected abstract void SetState(bool state);
    }
}