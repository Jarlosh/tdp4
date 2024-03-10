using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Loader
{
    public class LoaderView: MonoBehaviour
    {
        [SerializeField]
        private LineItem[] items;

        [SerializeField]
        private float duration = 2f;
        
        [SerializeField]
        private float duration2 = 1f;
        
        [SerializeField] 
        private Image Filled;

        [SerializeField] 
        private CanvasGroup Group;
        [SerializeField] 
        private CanvasGroup ExitGroup;

        [SerializeField] 
        private TMP_Dropdown Dropdown;
        
        public bool IsAnimating { get; set; }
        
        private float startTime;
        private float progress;
        private bool exit;

        private void Start()
        {
            Dropdown.onValueChanged.AddListener(Changed);

            StartCoroutine(GoGoGo());
        }

        private void Changed(int id)
        {
            startTime = Time.time;
            exit = true;
        }

        private IEnumerator GoGoGo() {
            for (var i = 0; i < 1; i++) {
                IsAnimating = true;
                startTime = Time.time;
                yield return new WaitForSeconds(3);
            }

            FindAnyObjectByType<GameScenesFlow>().NextScene();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //GoGoGo();
            }
            else if (IsAnimating)
            {
                var dt = Time.time - startTime;
                var dur = exit ? duration2 : duration;
                progress = Mathf.Clamp01(dt / dur);
            }

            if (exit)
            {
                ExitGroup.alpha = (1 - progress);
            }
            else if(IsAnimating)
            {
                Group.alpha = Mathf.InverseLerp(0.9f, 1, progress);
                SetValue(progress);
                Filled.fillAmount = (1 - progress);
            }
            else
            {
                SetValue(0);
            }
        }

        public void SetValue(float normalized)
        {
            var total = items.Sum(i => i.Weight);
            var weight = total * normalized;
            foreach (var item in items)
            {
                var progress = Mathf.Clamp01(weight / item.Weight);
                weight = Mathf.Max(0, weight - item.Weight);
                item.SetValue(progress);
            }
        }
    }
}