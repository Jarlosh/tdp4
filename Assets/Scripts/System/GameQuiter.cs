using UnityEngine;

public class GameQuiter : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit(0);
    }
}