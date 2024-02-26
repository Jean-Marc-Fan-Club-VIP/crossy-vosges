using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControllerAudio : MonoBehaviour
{
    public AudioClip sound;

    //private float volume;

    private AudioSource source;

    void Awake()
    {
        /*source = GetComponent<AudioSource>();

        volume = OptionsMenu.volumeSound;
        pitch = 1f;*/
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = OptionsMenu.volumeSound;
        
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = OptionsMenu.volumeSound;
    }

    public void PlayButton()
    {
        source.PlayOneShot(sound);
        Debug.Log("TESTTTTT");
    }
}
