using System;
using TDPF.TimeSwitch;
using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    public abstract class FuckUpSubsystemBase: IInitializable, IDisposable
    {
        [Inject] private FuckUpController _fuckController;
        [Inject] protected TimeSwitcher timeController;

        public virtual void Initialize()
        {
            _fuckController.Subscribe(OnActivation);
        }

        public virtual void Dispose()
        {
            _fuckController.Unsubscribe(OnActivation);
        }

        protected abstract void OnActivation(IActivateKey key);
    }
}