using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject objectToActivate;

    private void Start() 
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("StartAnimation");
        
        objectToActivate.SetActive(false);

        float animationDuration = GetAnimationDuration("Credits Move");
        Invoke("ActivateObject", animationDuration);

    }

    private float GetAnimationDuration(string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        return 0f;
    }

    private void ActivateObject()
    {
        objectToActivate.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
