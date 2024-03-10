using UnityEngine;
using Zenject;

namespace TDPF.FuckUp
{
    public class EventRoot : MonoBehaviour
    {
        [SerializeField] private ClueKey clueKey;
        [Inject] private FuckUpController _fuckUpController;

        private void Awake()
        {
            _fuckUpController.Activate(clueKey, gameObject);
        }
    }
}