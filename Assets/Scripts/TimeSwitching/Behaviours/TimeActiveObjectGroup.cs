using System;
using TDPF.Tools;
using UnityEngine;

namespace TDPF.TimeSwitch
{
    public class TimeActiveObjectGroup : TimeDependedObject
    {
        [Serializable]
        private class List
        {
            [SerializeField] private GameObject[] targets;
        
            [SerializeField] private MonoBehaviour[] targetComponents;

            public void SetState(bool state)
            {
                foreach (var target in targets)
                {
                    target.SetActive(state);
                }

                foreach (var component in targetComponents)
                {
                    component.enabled = state;
                }
            }
        }

        [SerializeField] private List beforeEnable;
        [SerializeField] private List futureEnable;
        
        public override void SwitchTo(TimeMoment target)
        {
            beforeEnable.SetState(target.IsPast());
            futureEnable.SetState(target.IsFuture());
        }
    }
}