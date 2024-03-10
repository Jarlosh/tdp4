using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TDPF.Player
{
    public class ZoomController: MonoBehaviour
    {
        [SerializeField] private List<CinemachineFollowZoom> cameras;
        [SerializeField] private InputActionReference action;
        [SerializeField] private float enabledZoom;
        [SerializeField] private float disabledZoom;
        [SerializeField] private float enabledPovSpeed;
        [SerializeField] private float disabledPovSpeed;
        [SerializeField] private float forcezoom;
        public bool Forced { get; private set; }
        private List<CinemachinePOV> povs = new();

        private void Awake()
        {
            povs = cameras.Select(c =>
                c.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>()).ToList();
        }

        private void OnEnable()
        {
            action.action.Enable();
            action.action.started += StartZoom;
            action.action.canceled += FinishZoom;
        }

        private void OnDisable()
        {
            action.action.Disable();
            action.action.started += StartZoom;
            action.action.canceled += FinishZoom;
        }

        public void Reset()
        {
            SetState(action.action.triggered);
        }

        private void FinishZoom(InputAction.CallbackContext obj)
        {
            SetState(false);
        }

        private void StartZoom(InputAction.CallbackContext obj)
        {
            SetState(true);
        }

        public void SetForced(bool state)
        {
            Forced = state;
            if (state)
            {
                SetState(true);
            }
            else
            {
                Reset();
            }
        }
        
        public void SetState(bool state)
        {
            if (Forced)
            {
                SetValue(forcezoom, enabledPovSpeed);
                return;
            }
            if(!state)
            {
                SetValue(disabledZoom, disabledPovSpeed);
            }
            else 
            {
                SetValue(enabledZoom, enabledPovSpeed);
            }
        }
        
        private void SetValue(float value, float povSpeed)
        {
            cameras.ForEach(c => c.m_Width = value);
            povs.ForEach(p => p.m_HorizontalAxis.m_MaxSpeed = povSpeed);
            povs.ForEach(p => p.m_VerticalAxis.m_MaxSpeed = povSpeed);
        }
    }
}