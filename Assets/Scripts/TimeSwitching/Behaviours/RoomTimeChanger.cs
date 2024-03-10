using Cinemachine;
using TDPF.Tools;
using UnityEngine;

namespace TDPF.TimeSwitch
{
    public enum CameraPriority
    {
        Inactive = 0,
        Active = 1
    }

    public class RoomTimeChanger : TimeDependedObject
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform pastRoom;
        [SerializeField] private Transform futureRoom;

        [SerializeReference] CinemachineVirtualCamera cameraForNow;
        [SerializeReference] CinemachineVirtualCamera cameraForPast;
        private TimeMoment _currentMomentSet = TimeSwitcher.InitialTimeMoment;

        public override void SwitchTo(TimeMoment target)
        {
            if (_currentMomentSet == target)
            {
                return;
            }
            UpdatePosition(target);
            UpdateCamera(target);
            _currentMomentSet = target;
        }

        private void UpdatePosition(TimeMoment target)
        {
            var (current, next) = target.Order(pastRoom, futureRoom);
            player.position = TransformPosition(player.position, current, next);
        }

        private void UpdateCamera(TimeMoment target)
        {
            var (current, next) = target.Order(cameraForPast, cameraForNow);
            next.Priority = (int)CameraPriority.Active;
            current.Priority = (int)CameraPriority.Inactive;

            var rotation = RoomTransformRotation(target, current.transform.forward);
            next.ForceCameraPosition(next.transform.position, rotation);
        }
        
        private Quaternion RoomTransformRotation(TimeMoment target, Vector3 forward)
        {
            var (currentRoom, nextRoom) = target.Order(pastRoom, futureRoom);
            forward = TransformRotation(forward, currentRoom, nextRoom);
            return Quaternion.LookRotation(forward, nextRoom.up);
        }
        
        private static Vector3 TransformPosition(Vector3 point, Transform current, Transform next)
        {
            return next.TransformPoint(current.InverseTransformPoint(point));
        }

        private static Vector3 TransformRotation(Vector3 direction, Transform current, Transform next)
        {
            return next.TransformDirection(current.InverseTransformDirection(direction));
        }
    }
}