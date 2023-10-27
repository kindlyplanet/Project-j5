using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public string sceneToRestart;

    public void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToRestart);
    }

}
