using TDPF.FuckUp;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.NotePad
{
    public class NotepadController: FuckUpBehaviour
    {
        [SerializeField]
        public InputActionReference inputAction;
        
        [SerializeField]
        public OpenAnimator openAnimator;
        
        [SerializeField]
        private Transform content;

        [SerializeField]
        private PadTab[] tabs;

        [SerializeField]
        public PadContent padContent;

        private bool _isOpen;
        
        private int CurrentIndex { get; set; }

        private PadTab ActiveTab => tabs[CurrentIndex];

        private void Awake()
        {
            for (var index = 0; index < tabs.Length; index++)
            {
                var padTab = tabs[index];
                var i = index;
                padTab.SetCallback(() => SetTab(i));
            }
            SetTab(0);
        }

        protected override void SetState(bool state)
        {
            foreach (var tab in tabs)
            {
                tab.OnResolved();
            }
            UpdateTabView();
        }

        private void SetTab(int index)
        {
            CurrentIndex = index;
            
            foreach (var tab in tabs)
            {
                tab.transform.SetAsLastSibling();
            }
            content.SetAsLastSibling();
            ActiveTab.transform.SetAsLastSibling();
            UpdateTabView();
        }

        private void UpdateTabView()
        {
            padContent.SetPage(CurrentIndex, ActiveTab.GetTexts());
        }

        private void OnEnable()
        {
            inputAction.action.Enable();
            inputAction.action.performed += OnPerformed;
        }

        private void OnDisable()
        {
            inputAction.action.Disable();
            inputAction.action.performed -= OnPerformed;
        }

        private void OnPerformed(InputAction.CallbackContext obj)
        {
            UpdateTabView();
            _isOpen = !_isOpen;
            openAnimator.Target = _isOpen ? 1 : 0;
        }
    }
}