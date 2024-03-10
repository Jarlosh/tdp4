using FMODUnity;
using TDPF.Player;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public class LightAttenuationAction: InteractAction
    {
        [Inject] private PlayerObj _playerObj;
        public StudioEventEmitter emitter;
        public float coefficient;

        protected override void Process()
        {
            var delta = emitter.transform.position - _playerObj.transform.position;
            emitter.OverrideMinDistance = delta.magnitude * coefficient + -0.5f;
            emitter.OverrideMaxDistance = delta.magnitude * coefficient + +0.5f;
        }
    }
}