using System;
using System.Collections;
using System.Collections.Generic;
using TDPF.Tools;
using UnityAsync;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using WaitForSeconds = UnityEngine.WaitForSeconds;

namespace TDPF.TimeSwitch
{
    [Flags]
    public enum TimeMoment
    {
        Future = 1 << 0, // 1
        Past = 1 << 1, // 2
    }
    
    public class TimeSwitcher : MonoBehaviour
    {
        public const TimeMoment InitialTimeMoment = TimeMoment.Future;
        
        public UnityAction<TimeMoment> OnStartSwitchingTo;
        public UnityAction<TimeMoment> OnSwitchHappenedTo;

        [SerializeField] private float delayFromStartToSwitch = 1;

        [SerializeField] private InputActionReference debugSwitchInput;

        [SerializeField] private FMODUnity.StudioEventEmitter pastAmbient;

        [SerializeField] private FMODUnity.StudioEventEmitter nowAmbient;

        private readonly List<ITimeDependent> _timeObjects = new();
        private Coroutine _switchCoroutine;

        private bool IsSwitching => _switchCoroutine != null;

        public TimeMoment CurrentMoment { get; set; } = InitialTimeMoment;

        private void Awake()
        {
            SwitchAmbient(CurrentMoment);

            #if UNITY_EDITOR
            debugSwitchInput.action.performed += (_) => GoTo(CurrentMoment.Invert());
            debugSwitchInput.action.Enable();
            #endif
        }

        private void OnDestroy()
        {
            pastAmbient.Stop();
            nowAmbient.Stop();
        }

        public Action SetSwitchCallbacks(Action<TimeMoment> onStartTransition, Action<TimeMoment> onEndTransition)
        {
            OnStartSwitchingTo += OnStart;
            OnSwitchHappenedTo += OnEnd;
            return () =>
            {
                OnStartSwitchingTo -= OnStart;
                OnSwitchHappenedTo -= OnEnd;
            };

            void OnStart(TimeMoment moment) => onStartTransition(moment);

            void OnEnd(TimeMoment moment) => onEndTransition(moment);
        }

        public void SetRegistered(ITimeDependent obj, bool state)
        {
            if (state)
            {
                Register(obj);
            }
            else
            {
                Unregister(obj);
            }
        }

        private void Register(ITimeDependent obj)
        {
            obj.SwitchTo(CurrentMoment);
            _timeObjects.Add(obj);
        }

        private void Unregister(ITimeDependent obj)
        {
            _timeObjects.Remove(obj);
        }
        
        public void GoTo(TimeMoment moment)
        {
            if (CurrentMoment == moment || IsSwitching)
            {
                return;
            }

            OnStartSwitchingTo?.Invoke(moment);
            _switchCoroutine = StartCoroutine(DelayAndSwitch(moment, delayFromStartToSwitch));
        }

        private IEnumerator DelayAndSwitch(TimeMoment targetMoment, float delayToSwitch)
        {
            yield return new WaitForSeconds(delayToSwitch);
            SetTime(targetMoment);
            _switchCoroutine = null;
        }

        private void SetTime(TimeMoment targetMoment)
        {
            if (targetMoment == CurrentMoment)
            {
                return;
            }

            CurrentMoment = targetMoment;
            SwitchTimeDependedObjects(targetMoment);
            SwitchAmbient(targetMoment);
            OnSwitchHappenedTo?.Invoke(targetMoment);
        }

        private void SwitchTimeDependedObjects(TimeMoment targetMoment)
        {
            foreach (var obj in _timeObjects.ToArray())
            {
                obj.SwitchTo(targetMoment);
            }
        }

        private void SwitchAmbient(TimeMoment targetMoment)
        {
            var active = targetMoment.IsPast() ? pastAmbient : nowAmbient;
            var notActive = targetMoment.IsPast() ? nowAmbient : pastAmbient;
            try
            {
                notActive.Stop();
                active.Play();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}