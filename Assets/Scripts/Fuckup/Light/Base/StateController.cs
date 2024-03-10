using System;
using System.Collections.Generic;
using System.Linq;
using TDPF.FuckUp.DialogueSystem;
using TDPF.Tools;
using UnityEngine;

namespace TDPF.FuckUp
{
    public class StateController: ListenObject
    {
        [SerializeField] private List<RestrictionsHolder> states;
        
        private readonly HashSet<RestrictionsHolder> _activeStates = new();
        private readonly HashSet<RestrictionsHolder> _temp = new();
        private readonly HashSet<RestrictionsHolder> _toRemove = new();

        private void Awake()
        {
            foreach (var state in states)
            {
                try
                {
                    state.SetObjectActive(false);
                }
                catch (Exception e)
                {
                    Debug.LogError(gameObject.GetScenePath());
                    throw;
                }
                // if (state.gameObject.activeSelf)
                // {
                //     // Debug.LogError($"{state.name} must be disabled!\n{state.GetScenePath()}");
                // }
            }
        }

        protected override void UpdateState()
        {
            _toRemove.Clear();
            _temp.Clear();
            CheckStates();
            foreach (var state in _activeStates)
            {
                if (!_temp.Contains(state))
                {
                    _toRemove.Add(state);
                    state.SetObjectActive(false);
                }
            }
            foreach (var state in _temp.ToArray())
            {
                if (!_activeStates.Contains(state))
                {
                    state.SetObjectActive(true);
                    _activeStates.Add(state);
                }
            }
            foreach (var state in _toRemove)
            {
                _activeStates.Remove(state);
            }
        }
        
        private void CheckStates()
        {
            RestrictionsHolder holder = null;
            try
            {
                foreach (var state in states)
                {
                    holder = state;
                    if (state.Restriction.AllPassing(timeController.CurrentMoment))
                    {
                        _temp.Add(state);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, holder);
            }
        }
    }
}