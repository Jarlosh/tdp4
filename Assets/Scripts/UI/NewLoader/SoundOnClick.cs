using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

namespace UI.NewLoader
{
    public class SoundOnClick: MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private EventReference clickSound;

        private void OnEnable()
        {
            button.onClick.AddListener(Play);
        }
        
        private void OnDisable()
        {
            button.onClick.RemoveListener(Play);
        }

        private void Play()
        {
            RuntimeManager.PlayOneShot(clickSound);
        }
    }
}