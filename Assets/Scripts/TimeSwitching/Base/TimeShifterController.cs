using System;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using TDPF.FuckUp;
using TDPF.TimeSwitch;
using TDPF.Tools;
using Zenject;

namespace TDPF.Player.Inventory
{
    public class TimeShifterController : ListenObject
    {
        [Inject] private PlayerObj _player;
        [Inject] private TimeSwitcher _timeSwitcher;
        
        [SerializeReference] private InputActionReference toThePastInput;
        [SerializeReference] private InputActionReference toTheNowInput;

        [SerializeReference] private GameObject timeShifterModel;

        [SerializeField] private StudioEventEmitter switchSound;
        [SerializeField] private StudioEventEmitter errorSound;
        [SerializeField] private StudioEventEmitter timeTravelSound;

        [SerializeField] private ClueKey tookShifterKey;

        private PauseMenuController _pauseController;

        private void Start()
        {
            toTheNowInput.action.SetSubscription(true, GoFuture);
            toThePastInput.action.SetSubscription(true, GoPast);
            _pauseController = FindAnyObjectByType<PauseMenuController>();
            _pauseController.OnPauseUpdated += UpdateState;
        }

        private void OnDestroy()
        {
            toTheNowInput.action.SetSubscription(true, GoFuture);
            toThePastInput.action.SetSubscription(true, GoPast);
            _pauseController.OnPauseUpdated -= UpdateState;
        }

        protected override void UpdateState()
        {
            var hasSwitcher = tookShifterKey.Active;
            toTheNowInput.action.SetEnabled(hasSwitcher);
            toThePastInput.action.SetEnabled(hasSwitcher);
            timeShifterModel.SetActive(hasSwitcher); 
        }

        private void GoPast(InputAction.CallbackContext obj)
        {
            ProcessTheButtonPressed(TimeMoment.Past);
        }

        private void GoFuture(InputAction.CallbackContext obj)
        {
            ProcessTheButtonPressed(TimeMoment.Future);
        }

        private void ProcessTheButtonPressed(TimeMoment button)
        {
            if (_player.InDialogue)
            {
                return;
            }
            switchSound.Play();

            if (_timeSwitcher.CurrentMoment == button)
            {
                errorSound.Play();
                return;
            }

            timeTravelSound.Play();
            _timeSwitcher.GoTo(button);
        }
    }
}