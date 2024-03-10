using System;
using UnityEngine.InputSystem;

namespace TDPF.Tools
{
    using Callback = Action<InputAction.CallbackContext>;
    public static class InputSubscriptionUtils
    {
        public static void SetEnabled(this InputAction action, bool state)
        {
            if (state)
            {
                action.Enable();
            }
            else
            {
                action.Disable();
            }
        }
        
        public static void SetSubscription(this InputAction action, bool state, 
            in Callback startCallback = null, 
            in Callback performCallback = null,
            in Callback cancelCallback = null)
        {
            SetStartSubscription(action, state, startCallback);
            SetPerformSubscription(action, state, performCallback);
            SetCancelSubscription(action, state, cancelCallback);
        }

        public static void SetStartSubscription(this InputAction action, bool state, in Callback callback)
        {
            if (callback == null)
            {
                return;
            }

            if (state)
            {
                action.started += callback;
            }
            else
            {
                action.started -= callback;
            }
        }

        public static void SetPerformSubscription(this InputAction action, bool state, in Callback callback)
        {
            if (callback == null)
            {
                return;
            }

            if (state)
            {
                action.performed += callback;
            }
            else
            {
                action.performed -= callback;
            }
        }

        public static void SetCancelSubscription(this InputAction action, bool state, in Callback callback)
        {
            if (callback == null)
            {
                return;
            }

            if (state)
            {
                action.canceled += callback;
            }
            else
            {
                action.canceled -= callback;
            }
        }
    }
}