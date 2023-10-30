using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WallButtom : MonoBehaviour
{   

   public float tiempoDeCambio;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {      
            Invoke("CambiarEscena",tiempoDeCambio);
        }
    }
    public void CambiarEscena()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        int currentSceneIndex = currentScene.buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
       
        }
        else
        {
            Debug.LogWarning("No hay una siguiente escena disponible");
        }
    }

  
}
