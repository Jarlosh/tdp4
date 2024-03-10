using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenesFlow : MonoBehaviour
{
    private ushort currensSceneIndex = 0;

    public void NextScene()
    {
        if (currensSceneIndex >= 2)
        {
            return;
        }

        currensSceneIndex += 1;
        SceneManager.LoadScene(currensSceneIndex);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Numlock))
        {
            NextScene();
        }
    }
    #endif
}