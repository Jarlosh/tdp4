using UnityEngine;
using UnityEngine.Events;

namespace TDPF.FuckUp
{
    public class UnityEventTriggerByScenarioAction : FuckUpBehaviour
    {
        [SerializeField] UnityEvent OnKeyActivated;
        [SerializeField] UnityEvent OnKeyDeactivated;

        protected override void SetState(bool state)
        {
            (state ? OnKeyActivated : OnKeyDeactivated).Invoke();
        }
    }
}