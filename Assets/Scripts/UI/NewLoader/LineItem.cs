using UnityEngine;
using UnityEngine.UI;

namespace UI.Loader
{
    public class LineItem: MonoBehaviour
    {
        public Image image;
        [SerializeField] private RectTransform rt;
        
        public float Weight => ((rt != null ? rt : rt = transform as RectTransform)!).rect.height;

        public void SetValue(float normalized)
        {
            image.fillAmount = normalized;
        }
    }
}