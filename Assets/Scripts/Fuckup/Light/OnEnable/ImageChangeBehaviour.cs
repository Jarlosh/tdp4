using UnityEngine;
using UnityEngine.UI;

namespace TDPF.FuckUp
{
    public class ImageChangeOnEnable: ListenObject
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite sprite;

        protected override void UpdateState()
        {
            image.sprite = sprite;
        }
    }
}