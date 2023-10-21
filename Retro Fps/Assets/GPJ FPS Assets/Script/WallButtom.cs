using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WallButtom : MonoBehaviour
{

    [SerializeField] private string changeScene;
    [SerializeField] private float timer;
    
    
    private void Start() 
    {
          
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
          StartCoroutine(WaitAndLoadScene());
        }
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(timer);
        
        SceneManager.LoadScene(changeScene);

    }
}
