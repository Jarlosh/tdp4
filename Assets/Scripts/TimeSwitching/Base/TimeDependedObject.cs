using UnityEngine;
using UnityEngine.Events;

namespace TDPF.TimeSwitch
{
    public interface ITimeDependent
    {
        void SwitchTo(TimeMoment target);
    }

    public abstract class TimeDependedObject : MonoBehaviour, ITimeDependent
    {
        protected TimeSwitcher Switcher { get; private set; }
        
        protected TimeMoment CurrentMoment => Switcher.CurrentMoment;

        protected virtual void Start()
        {
            Switcher = FindObjectOfType<TimeSwitcher>();
            Switcher.SetRegistered(this, true);
            SwitchTo(Switcher.CurrentMoment);
            SetTimeSubscribed(true);
        }

        protected virtual void OnDestroy()
        {
            Switcher.SetRegistered(this, false);
            SetTimeSubscribed(false);
        }

        // optional
        protected virtual void SetTimeSubscribed(bool state)
        {
        }

        public abstract void SwitchTo(TimeMoment target);
    }
}