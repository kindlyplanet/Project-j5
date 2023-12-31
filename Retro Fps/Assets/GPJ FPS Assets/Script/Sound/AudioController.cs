using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public Sound[] musicSounds,sfxSounds;
    public AudioSource musicSource, sfxSource;
    public string selectMusic;
    
    
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(selectMusic);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds,x=> x.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x=> x.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

   

  
}
