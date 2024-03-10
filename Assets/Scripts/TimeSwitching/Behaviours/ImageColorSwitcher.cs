using TDPF.TimeSwitch;
using TDPF.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageColorSwitcher: TimeDependedObject
    {
        [SerializeField] 
        private Image image;

        [SerializeField] 
        private Color colorBefore;

        [SerializeField] 
        private Color colorAfter;

        public override void SwitchTo(TimeMoment target)
        {
            image.color = target.IsPast() ? colorBefore : colorAfter;
        }
    }
}