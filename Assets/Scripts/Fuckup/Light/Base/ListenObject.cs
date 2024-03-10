using TDPF.FuckUp.DialogueSystem;
using TDPF.TimeSwitch;
using UnityEngine;
using Zenject;

namespace TDPF.FuckUp
{
    public abstract class ListenObject : MonoBehaviour, ITimeDependent
    {
        [Inject] protected FuckUpController fuckUpController;
        [Inject] protected TimeSwitcher timeController;

        public TimeMoment CurrentMoment => timeController.CurrentMoment;

        private void OnEnable()
        {
            fuckUpController.Subscribe(UpdateState);
            timeController.SetRegistered(this, true);
            UpdateState(null);
        }

        protected void OnDisable()
        {
            fuckUpController.Unsubscribe(UpdateState);
            timeController.SetRegistered(this, false);
        }

        public void SwitchTo(TimeMoment target) => UpdateState();
        private void UpdateState(IActivateKey _) => UpdateState();
        protected abstract void UpdateState();
    }
}