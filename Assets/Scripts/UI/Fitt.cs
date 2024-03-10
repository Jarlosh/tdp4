using UnityEngine;

namespace UI
{
    public class Fitt: MonoBehaviour
    {
        public bool horizontal;
        public bool vertical;
        [SerializeField] private RectTransform current;
        [SerializeField] private RectTransform source;

        private void UpdateUI()
        {
            current.sizeDelta = new Vector2(
                horizontal ? current.sizeDelta.x : source.sizeDelta.x,
                vertical ? current.sizeDelta.y : source.sizeDelta.y);
        }
        
        protected void OnValidate()
        {
            UpdateUI();
        }
    }
}