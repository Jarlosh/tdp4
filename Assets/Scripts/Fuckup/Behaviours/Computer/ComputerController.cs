using UnityEngine;

namespace TDPF.FuckUp
{
    public class ComputerController : MonoBehaviour
    {
        public enum PcAction
        {
            StartPress,
            Examine,
            Submit,
            FinishGame,
        }

        public void Interact(PcAction pcAction)
        {
            if (pcAction is PcAction.FinishGame)
            {          
                FinishGame();
            }
        }
        
        private void FinishGame()
        {
            FindAnyObjectByType<GameScenesFlow>().NextScene();
        }
    }
}