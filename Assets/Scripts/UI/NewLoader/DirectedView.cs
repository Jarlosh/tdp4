using UnityAsync;
using UnityEngine;

namespace UI.NewLoader
{
    public class DirectedView: MonoBehaviour
    {
        public float delay;
        public GameObject disable;
        public GameObject enable;
        
        private void Awake() => Load();

        public async void Load()
        {
            await Await.Seconds(delay);
            disable.SetActive(false);
            enable.SetActive(true);
        }
    }
}