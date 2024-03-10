using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace TDPF.Player
{
    public class PlayerObj: MonoBehaviour
    {
        [SerializeField] private Transform cameraParent;
        [SerializeField] private ZoomController zoomController;
        [SerializeField] private List<CinemachineVirtualCamera> cameras;
        
        private List<CinemachinePOV> _povs = new();
        private Transform _dialogueSoundSource;

        public Transform DialogueSoundSource
        {
            get => _dialogueSoundSource;
            set
            {
                _dialogueSoundSource = value;
                if (_dialogueSoundSource == null)
                {
                    _dialogueSoundSource = cameraParent.transform;
                }
                UpdateState();
            }
        }

        public bool InDialogue { get; set; }
        public bool HasTarget => DialogueSoundSource != null && _dialogueSoundSource != cameraParent.transform;
        private Quaternion _rotationTarget;
        
        private void Awake()
        {
            _povs = cameras.Select(c => c.GetCinemachineComponent<CinemachinePOV>()).ToList();
            UpdateState();
        }

        private void UpdateState()
        {
            if(HasTarget)
            {
                // var targetPos = _dialogueSoundSource.transform.position;
                // var delta = targetPos - cameraParent.transform.position;
                // var toSource = Proj(delta);
                // var active = cameras[0].Priority == 1 ? 1 : 0;
                // var cam = cameras[active].transform;
                // var camRot = Proj(cam.forward);
                // var r = Quaternion.FromToRotation(camRot, toSource);
                // var horSide = new Plane(cam.right, cam.position).GetSide(targetPos);
                // var vSide = new Plane(cam.up, cam.position).GetSide(targetPos);
                // cameraParent.rotation = Quaternion.LookRotation(toSource, Vector3.up);
                // var pow = _povs[active];
                // r.ToAngleAxis(out var ha, out _);
                // pow.m_HorizontalAxis.Value = ((ha + 180) % 180) * -(horSide ? 1 : -1);
            }
            // zoomController.SetForced(HasTarget);

            foreach (var pov in _povs)
            {
                // pov.m_HorizontalRecentering.m_enabled = HasTarget;
                // pov.m_VerticalRecentering.m_enabled = HasTarget;
            }

            Vector3 Proj(Vector3 v) => Vector3.ProjectOnPlane(v, Vector3.up);
            Quaternion ProjQ(Vector3 v) => Quaternion.LookRotation(Proj(v), Vector3.up);
        }
    }
}