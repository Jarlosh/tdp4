using TDPF.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace TDPF.TimeSwitch
{
    public class TimeSwitchUnityEvents: TimeDependedObject
    {
        [SerializeField] public UnityEvent onSwitchedToNow;
        [SerializeField] public UnityEvent onSwitchedToPast;

        public override void SwitchTo(TimeMoment target)
        {
            if (target.IsPast())
            {
                onSwitchedToPast?.Invoke();
            }

            if (target.IsFuture())
            {
                onSwitchedToNow?.Invoke();
            }
        }
    }
}