
using TDPF.Tools;
using UnityEngine;

namespace TDPF.TimeSwitch
{
    [RequireComponent(typeof(Skybox))]
    public class SkyboxManager: TimeDependedObject
    {
        [SerializeField]
        private Material pastMaterial;
        
        [SerializeField]
        private Material futureMaterial;
        private Skybox _skybox;

        private void Awake()
        {
            _skybox = GetComponent<Skybox>();
        }

        public override void SwitchTo(TimeMoment target)
        {
            _skybox.material = target.IsPast() ? pastMaterial : futureMaterial;
        }
    }
}