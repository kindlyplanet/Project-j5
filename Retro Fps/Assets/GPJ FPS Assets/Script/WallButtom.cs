using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WallButtom : MonoBehaviour
{

    [SerializeField] private string changeScene;
    [SerializeField] private GameObject endTransition;
    [SerializeField] private float waitingTime; 

    private bool canLoadScene = false;

    private void Start() 
    {
        endTransition.SetActive(false);    
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !canLoadScene)
        {
           StartCoroutine(PlayTransitionAndLoadScene());
        }
    }

    private IEnumerator PlayTransitionAndLoadScene()
    {
        
        endTransition.SetActive(true);
        Debug.Log("activo");

        yield return new WaitForSeconds(waitingTime);
        
        SceneManager.LoadScene(changeScene);

        canLoadScene = true;
    }
}
