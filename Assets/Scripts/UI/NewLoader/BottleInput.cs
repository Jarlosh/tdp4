using TDPF.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.NewLoader
{
    public class BottleInput: MonoBehaviour
    {
        [SerializeField] private BottleController bottle;
        [SerializeField] private InputActionReference pewAction;
        
        private void OnEnable() => SetState(true);

        private void OnDisable() => SetState(false);

        private RectTransform _bottleTransform;

        private void Awake()
        {
            _bottleTransform = bottle.GetComponent<RectTransform>();
        }

        private void SetState(bool state)
        {
            pewAction.action.SetEnabled(state);
            pewAction.action.SetSubscription(state, 
                performCallback: Unblock, 
                cancelCallback: Block);
        }
        
        private void Block(InputAction.CallbackContext obj)
        {
            bottle.Blocked = true;
        }

        private void Unblock(InputAction.CallbackContext obj)
        {
            bottle.Blocked = false;
        }

        private void Update()
        {
            _bottleTransform.anchoredPosition = GetMousePosition();
        }
        
        private Vector2 GetMousePosition()
        {
            var res = Screen.currentResolution;
            return new Vector2(
                Input.mousePosition.x * 1920 / Screen.width,
                Input.mousePosition.y * 1080 / Screen.height);
        }
    }
}