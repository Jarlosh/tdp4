using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWithUnityEvent : Trigger
{
    [SerializeField]
    UnityEvent OnTriggered;

    protected override void Process() {
        OnTriggered?.Invoke();
    }
}
