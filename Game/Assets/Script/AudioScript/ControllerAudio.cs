using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControllerAudio : MonoBehaviour
{
    public AudioClip sound;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = OptionsMenu.volumeSound;
        
    }

    void Update()
    {
        source.volume = OptionsMenu.volumeSound;
    }

    


    public void PlaySound()
    {
        if (sound && source)
        {

            StartCoroutine(WaitSoundToFinishPlaying());
        }
    }

    public IEnumerator WaitSoundToFinishPlaying()
    {
        source.PlayOneShot(sound);
        yield return new WaitForSeconds(sound.length); 
    }
}
