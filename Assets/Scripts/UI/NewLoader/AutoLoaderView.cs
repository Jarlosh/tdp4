using System;
using System.Linq;
using System.Threading.Tasks;
using FMODUnity;
using TMPro;
using UI.Loader;
using UnityAsync;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.NewLoader
{
    public class AutoLoaderView: MonoBehaviour
    {
        [SerializeField] 
        private StudioEventEmitter emitter;
        
        [SerializeField] 
        private EventReference columnSound;
        
        [SerializeField] 
        private EventReference clickSound;
        
        [SerializeField]
        private LineItem[] items;

        [SerializeField] 
        private AnimationCurve bottlesCurve;

        [SerializeField] 
        private float maxBottlesRate;
            
        [SerializeField]
        private float animationDuration = 2f;

        [SerializeField] 
        private CanvasGroup UnityGrayGroup;
        [SerializeField] 
        private CanvasGroup DisclaimerGroup;
        [SerializeField] 
        private CanvasGroup DisclaimerBackGroup;
        
        [SerializeField] 
        private CanvasGroup[] Groups;
        
        [SerializeField] 
        private UiPulse DropdownPulse;
        
        [SerializeField] 
        private UiPulse ExitPulse;
        
        [SerializeField] 
        private Image beerFiller;
        
        [SerializeField] 
        private CanvasGroup exitHintGroup;
        
        [SerializeField] 
        private CanvasGroup dropdownGroup;
        
        [SerializeField] 
        private CanvasGroup exitGroup;

        [SerializeField] 
        private TMP_Dropdown dropdown;

        [SerializeField] 
        private AnimationCurve defaultCurve;
        
        [SerializeField] 
        private InputActionReference WakeUpAction;
        
        [SerializeField] 
        private BottleRotate[] Rotators;

        [SerializeField] 
        private Button SpaceButton;

        private bool animationComplete, entered, exiting;

        private int lastDoneCount = -1;
        
        private void Start()
        {
            Cursor.visible = false;
            Play();
            WakeUpAction.action.Enable();
            WakeUpAction.action.performed += OnPressed;
            dropdown.onValueChanged.AddListener(OnChanged);
            SpaceButton.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            WakeUpAction.action.performed -= OnPressed;
            dropdown.onValueChanged.RemoveListener(OnChanged);
            SpaceButton.onClick.RemoveListener(OnClick);
        }
        
        private void OnChanged(int arg0)
        {
            entered = true;
            ExitPulse.Active = true;
            // DropdownPulse.Active = false;
        }

        private async void OnClick()
        {
            if(entered && animationComplete && !exiting)
            {
                exiting = true;
                RuntimeManager.PlayOneShot(clickSound);
                await ShowGroup(exitGroup, 0.5f, true);
                Finish();
            }
        }
        
        private void OnPressed(InputAction.CallbackContext obj) => OnClick();

        private async void Play()
        {
            Reset();
            await Await.Seconds(0.1f);
            emitter.Play();
            await ShowGroup(UnityGrayGroup, 0.25f, true);
            await ShowGroup(DisclaimerGroup, 1f, false);
            await Await.Seconds(3);
            await ShowGroup(DisclaimerBackGroup, 1f, true);
            SetBottlesState(0.25f, true);
            await Await.Seconds(0.5f);
            await Animate();
            SetBottlesState(0.5f, false);
            await Task.WhenAll(Groups.Select(g => ShowGroup(g, 0.25f, false)));
            // DropdownPulse.Active = true;
            // await Cycle(() => entered);
            entered = true;
            
            await Task.WhenAll(
                // ShowGroup(dropdownGroup, 0.5f, true),
                ShowGroup(exitHintGroup, 0.5f, false));
            await Await.Seconds(3f);
            animationComplete = true;
            OnClick();
        }

        private void SetBottlesState(float duration, bool state)
        {
            foreach (var bottleRotate in Rotators)
            {
                bottleRotate.Do(duration, state);
            }
        }

        private void SetBottlesRate(float rate)
        {
            foreach (var bottleRotate in Rotators)
            {
                bottleRotate.SetRate(rate);
            }
        }

        private void Reset()
        {
            SetBottlesRate(0);
            UnityGrayGroup.gameObject.SetActive(true);
            UnityGrayGroup.alpha = 1;
            DisclaimerBackGroup.gameObject.SetActive(true);
            DisclaimerGroup.alpha = 0;
            foreach (var g in Groups)
            {
                g.alpha = 0;
            }
            foreach (var item in items)
            {   
                item.SetValue(0);
            }
        }

        private void Finish()
        {
            FindAnyObjectByType<GameScenesFlow>().NextScene();
        }

        private Task ShowGroup(CanvasGroup group, float duration, bool inversed)
        {
            return Cycle(Update, duration);

            void Update(float progress)
            {
                group.alpha = inversed ? 1 - progress : progress;
            }
        }

        private Task Animate()
        {
            // ShowGroup(dropdownGroup, animationDuration, true);
            
            return Cycle(SetColumnsValue, animationDuration);
        }

        private void SetColumnsValue(float normalized)
        {
            var total = items.Sum(i => i.Weight);
            var weight = total * normalized;
            var count = 0;
            foreach (var item in items)
            {
                var progress = Mathf.Clamp01(weight / item.Weight);
                if (Mathf.Approximately(progress, 1))
                {
                    count++;
                }
                weight -= progress * item.Weight;
                item.SetValue(progress);
            }

            if (count > lastDoneCount)
            {
                RuntimeManager.PlayOneShot(columnSound);
                lastDoneCount = count;
            }

            beerFiller.fillAmount = bottlesCurve.Evaluate(normalized+0.1f);
            SetBottlesRate(bottlesCurve.Evaluate(normalized) * maxBottlesRate);
        }

        private Task Cycle(Action<float> update, float duration, bool skipFirst = true)
        {
            return Cycle(update, defaultCurve, duration, skipFirst);
        }
        
        private Task Cycle(Action<float> update, AnimationCurve curve, float duration, bool skipFirst = true)
        {
            var progress = 0f;
            return Cycle(FrameProcessor, skipFirst);

            bool FrameProcessor()
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / duration);
                update(curve.Evaluate(progress));
                return progress >= 1;
            }
        }
        
        private async Task Cycle(Func<bool> frameProcessor, bool skipFirst = true)
        {
            if (skipFirst)
            {
                await Await.NextUpdate();
            }
            while (!frameProcessor())
            {
                await Await.NextUpdate();
            }
        }
    }
}