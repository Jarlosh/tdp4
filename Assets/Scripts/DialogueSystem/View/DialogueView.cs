using System.Linq;
using FMOD.Studio;
using FMODUnity;
using TDPF.Player;
using TDPF.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    public class DialogueView: ViewModelComponent<IDialogueItem>
    {
        [Inject] private PlayerObj _player;
        [SerializeField] private GameObject parent;
        [SerializeField] private TMP_Text textField;
        [SerializeField] private SpeakerView speakerView;
        [SerializeField] private CharacterVoiceView[] voices;
        [SerializeField] private CharacterVoiceView fallbackVoice;
        [SerializeField] private CharacterVoiceView allVoices;
        public bool playDifferentVoices = true;

        private SoundStrategy SoundMode => Model?.Settings?.Strategy ?? SoundStrategy.Never;
        private bool SoundValid => !Model?.Settings?.SoundEvent.IsNull ?? false;
        private EventReference Sound => SoundValid ? Model.Settings.SoundEvent: default;
        protected override GameObject Parent => parent;

        protected override void OnDataSet()
        {
            speakerView.SetData(Model.Character);
            textField.SetText(Model.Text);
            PlayVoice();
            // RuntimeManager.GetEventDescription(Sound).
        }

        public override void ClearData()
        {
            // if (SoundValid && SoundMode is SoundStrategy.After)
            // {
            //     Play(Sound);
            // }
            base.ClearData();
        }

        void Play(EventReference reference)
        {
            RuntimeManager.PlayOneShot(reference);
            // var source = (_player.DialogueSoundSource != null ? _player.DialogueSoundSource : _player.gameObject);
            // RuntimeManager.PlayOneShotAttached(reference, source);
            // var instance = RuntimeManager.CreateInstance(reference);
            // RuntimeManager.AttachInstanceToGameObject(instance, source.transform);
            // instance.getMinMaxDistance()
            // instance.start();
            // instance.getVolume(out var volume);
            // RuntimeManager.AttachInstanceToGameObject(instance, transform);
            // instance.setVolume(10);
            // instance.start();
        }
        
        private void PlayVoice()
        {
            var characterItem = Model.Character;
            // if (SoundValid && SoundMode is SoundStrategy.SameTime)
            // {
            //     Play(Sound);
            // }

            var silence = Model.Settings?.SilenceVoice ?? false;
            if (silence)
            {
                return;
            }
            if (!playDifferentVoices)
            {
                allVoices.Play();
            }
            else
            {
                var voice = voices.FirstOrDefault(v => v.Character.Equals(characterItem)) ?? fallbackVoice;
                if (voice != null)
                {
                    voice.Play();
                }
            }
        }
    }
}