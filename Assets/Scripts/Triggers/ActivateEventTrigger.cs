using TDPF.Tools;
using Zenject;

namespace TDPF.FuckUp.Interaction
{
    public class ActivateEventTrigger : Trigger
    {
        [Inject] private FuckUpController _fuckUpController;
        public ClueKey toActivate;

        public ClueKey[] restrictions;

        protected override void Process()
        {
            if (restrictions.AllPassing())
            {
                _fuckUpController.Activate(toActivate, gameObject);
            }
        }
    }
}