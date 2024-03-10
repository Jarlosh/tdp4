namespace TDPF.TimeSwitch
{
    public class DisabledDuringTimeChange: TimeDependedObject
    {
        protected override void SetTimeSubscribed(bool state)
        {
            Switcher.SetSwitchCallbacks(
                _ => gameObject.SetActive(false),
                _ => gameObject.SetActive(true));
        }

        public override void SwitchTo(TimeMoment target)
        {
        }
    }
}