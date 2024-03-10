using UnityEngine;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public class LightActivateClueAction: InteractAction
    {
        [Inject] private FuckUpController _fuckUpController;

        [SerializeField] private ClueKey toActivate;
        [SerializeField] private bool fireOnce = true;
        private bool _used;

        protected override void Awake()
        {
            base.Awake();
            _used = false;
        }

        protected override void Process()
        {
            if (enabled && (!fireOnce || !_used))
            {
                _fuckUpController.Activate(toActivate, gameObject);
                _used = true;
            }
        }
    }
}