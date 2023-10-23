using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WallButtom : MonoBehaviour
{

    [SerializeField] private string changeScene;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
          SceneManager.LoadScene(changeScene);
        }
    }

  
}
