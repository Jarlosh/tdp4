using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TDPF.FuckUp
{
    public class LoadNextSceneOnEnable: MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}