using System;
using System.Linq;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TDPF.FuckUp.DialogueSystem
{
    [Serializable]
    public class CharacterVoiceView: MonoBehaviour
    {
        private enum Strategy { PlayEmitters, PlayEvents}
        
        [SerializeField] private StudioEventEmitter[] samples;
        [SerializeField] private EventReference[] orEvents;
        [SerializeField] private Strategy strategy;
        [field: SerializeField] public CharacterItem Character { get; private set; }

        public void Play()
        {
            try
            {
                if (strategy is Strategy.PlayEmitters)
                {
                    var sounds = samples.Where(e => e != null).ToArray();
                    if (sounds.Any())
                    {
                        var sound = sounds[Random.Range(0, sounds.Length)];
                        sound.Play();
                    }
                }
                else if (strategy is Strategy.PlayEvents)
                {
                    var sounds = orEvents.Where(e => !e.IsNull).ToArray();
                    if (sounds.Any())
                    {
                        var sound = sounds[Random.Range(0, sounds.Length)];
                        RuntimeManager.PlayOneShot(sound);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}