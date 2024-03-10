using System.Collections;
using UnityEngine;

namespace TDPF.Utils
{
    public class SoundEventRepeater : MonoBehaviour
    {
        [SerializeReference] FMODUnity.StudioEventEmitter soundToRepeat;

        [SerializeField] float intervalInSec = 5;

        [SerializeField] bool activateOnAwake = false;
        [SerializeField] bool activateOnEnable = false;

        private Coroutine repeatCoroutine;

        private void Awake()
        {
            if (activateOnAwake)
            {
                Activate();
            }
        }

        private void OnEnable()
        {
            if (activateOnEnable)
            {
                Activate();
            }
        }

        private void OnDisable()
        {
            Deactivate();
        }

        public void Activate()
        {
            if (repeatCoroutine != null)
            {
                return;
            }

            repeatCoroutine = StartCoroutine(Repeating());
        }

        public void Deactivate()
        {
            if (repeatCoroutine == null)
            {
                return;
            }

            StopCoroutine(repeatCoroutine);
            repeatCoroutine = null;
        }

        private IEnumerator Repeating()
        {
            while (true)
            {
                soundToRepeat.Play();
                yield return new WaitForSeconds(intervalInSec);
            }
        }
    }
}