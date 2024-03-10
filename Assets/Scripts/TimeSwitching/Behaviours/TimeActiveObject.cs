using UnityEngine;

namespace TDPF.TimeSwitch
{
    public class TimeActiveObject : TimeDependedObject
    {
        [SerializeField] private TimeMoment enableMoment;
        
        public override void SwitchTo(TimeMoment target)
        {
            gameObject.SetActive((target & enableMoment) != 0);
        }
    }
}