using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public string sceneToRestart;

    public void ResetScene()
    {
        SceneManager.LoadScene(sceneToRestart);
    }

}
