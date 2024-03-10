using TDPF.FuckUp.DialogueSystem;
using TDPF.TimeSwitch;
using UnityEngine;
using Zenject;

namespace TDPF.FuckUp
{
    public class RestrictionsHolder: MonoBehaviour
    {
        [Inject] private TimeSwitcher _timeSwitcher;
        [field: SerializeField] public ClueRestriction Restriction { get; private set; }

        public bool AllPassing => Restriction.AllPassing(_timeSwitcher.CurrentMoment);
        
        public void SetObjectActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        [ContextMenu(nameof(CheckPast))]
        private void CheckPast() => Restriction.Check(TimeMoment.Past);

        [ContextMenu(nameof(CheckFuture))]
        private void CheckFuture() => Restriction.Check(TimeMoment.Future);
    }
}