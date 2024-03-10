using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.NotePad
{
    public class PadContent: MonoBehaviour
    {
        public List<TMP_Text> texts; 
        public Transform content;
        public GameObject prefab;
        
        public void SetPage(int index, string[] toAdd)
        {
            Clear();
            foreach (var text in toAdd)
            {
                texts.Add(Create(text));
            }
        }

        private void Clear()
        {
            foreach (var text in texts)
            {
                Destroy(text.gameObject);
            }
            texts.Clear();
        }

        private TMP_Text Create(string text)
        {
            var go = Instantiate(prefab, content);
            var comp = go.GetComponent<TMP_Text>();
            comp.text = text;
            return comp;
        }
    }
}