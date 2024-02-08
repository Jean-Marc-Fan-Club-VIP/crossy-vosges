using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControllerAudio : MonoBehaviour
{
    public AudioClip sound;

    private float volume;

    private float pitch;

    private AudioSource source;

    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();

        volume = 0.5f;
        pitch = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        source.clip = sound;
        source.volume = volume;
        source.pitch = pitch;
        
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = OptionsMenu.volumeSound;
    }

    public void PlayAndPause()
    {
        if(!source.isPlaying)
        {
            source.Play();
        }
        else
        {
            source.Pause();
        }
    }
}
