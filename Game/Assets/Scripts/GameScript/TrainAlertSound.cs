using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAlertSound : MonoBehaviour
{
    private GameObject player;
    public AudioClip sound;
    private AudioSource audioSource;
    private bool soundPlayed;
    public float delayBeforeSound = 0.4f;
    private float delayTimer = 0f;
    public int soundDistance = 5;
    private bool shouldPlaySound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        if (player != null &&
            player.transform.position.x >= transform.position.x - soundDistance &&
            player.transform.position.x <= transform.position.x + 1
        )
        {
            shouldPlaySound = true;
        }
    }

    private void Update()
    {
        if (shouldPlaySound && !soundPlayed)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer < delayBeforeSound)
            {
                return;
            }
            soundPlayed = true;
            audioSource.volume = OptionsMenu.volumeSound;
            audioSource.PlayOneShot(sound);
        }
    }
}
