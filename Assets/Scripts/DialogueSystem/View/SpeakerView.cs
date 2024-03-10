using TDPF.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TDPF.FuckUp.DialogueSystem
{
    public class SpeakerView: ViewModelComponent<CharacterItem>
    {
        [SerializeField] private TMP_Text speakerText;
        [SerializeField] private Image coloredImage;
        
        protected override void OnDataSet()
        {
            speakerText.SetText(Model.Name);
            coloredImage.color = Model.Color;
        }
    }
}