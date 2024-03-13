using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    private AudioSource backgroundMusic;
    
    private void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    public void PauseBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause();
        }
    }
}
