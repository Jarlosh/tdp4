using UnityEngine;

namespace TDPF.TimeSwitch
{
    public class TimeSwitchUiFadeController : MonoBehaviour
    {
        private Animator animController;

        private const string TriggerToNow = "ToNow";
        private const string TriggerToPast = "ToPast";
        private static readonly int ToNow = Animator.StringToHash(TriggerToNow);
        private static readonly int ToPast = Animator.StringToHash(TriggerToPast);

        private void Awake()
        {
            animController = GetComponent<Animator>();
            FindObjectOfType<TimeSwitcher>().OnStartSwitchingTo += ProcessStartSwitchingTo;
        }

        private void ProcessStartSwitchingTo(TimeMoment timeMoment)
        {
            var key = timeMoment == TimeMoment.Future ? ToNow : ToPast;
            animController.SetTrigger(key);
        }
    }
}