using FMODUnity;
using UnityEngine;

namespace TDPF.FuckUp
{
    public class TVController : FuckUpBehaviour
    {
        [System.Serializable]
        public enum TVState
        {
            Idle,
            Broken,
            MortalCombat
        }

        [SerializeReference] GameObject brokenGlassObj;
        [SerializeReference] GameObject mortalCombatObj;
        
        [SerializeField] 
        private TVState initialState = TVState.Broken;
        
        [SerializeField] 
        private bool isSoundMutedOnInit = true;
        
        [SerializeReference] 
        StudioEventEmitter mortalCombatSounds;

        private bool _isMuted;
        private TVState _currentState;

        private void Awake()
        {
            SetTVState(initialState);
            SetSoundMute(isSoundMutedOnInit);
        }
        
        protected override void SetState(bool state)
        {
            SetTVState(state ? TVState.MortalCombat : TVState.Broken);
        }
        
        private void SetTVState(TVState state)
        {
            _currentState = state;
            brokenGlassObj.SetActive(state == TVState.Broken);
            mortalCombatObj.SetActive(state == TVState.MortalCombat);

            if (state == TVState.MortalCombat && !_isMuted)
            {
                mortalCombatSounds.Play();
            }
            else
            {
                mortalCombatSounds.Stop();
            }
        }

        public void SetSoundMute(bool muted)
        {
            _isMuted = muted;

            if (!_isMuted && _currentState == TVState.MortalCombat)
            {
                mortalCombatSounds.Play();
            }
            else
            {
                mortalCombatSounds.Stop();
            }
        }
    }
}