using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public GameObject eagle;
    public GameObject player;
    public AudioClip sound;
    private AudioSource audioSource;

    public static bool isReady;
    private float speed = 6f;
    private bool soundIsPlayed = false;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 3f,  -1f);
        isReady = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(player != null)
        {
            if (isReady)
            {
                PlayEagleSound();

                float deltaX = speed * Time.deltaTime;
                transform.position += new Vector3(deltaX, 0f, 0f);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x - 12f, 3f, -1f);
            }
            //check if eagle has finished  its flight 
            if (transform.position.x > player.transform.position.x + 9)
            {
                Destroy(gameObject);
                Destroy(player);
            }

            // check if player and eagle is in same x position
            int playerX = (int)player.transform.position.x;
            int eagleX = (int)transform.position.x + 2;
            if (Mathf.Approximately(playerX, eagleX))
            {
                SetPlayerTransparency(); 
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void SetPlayerTransparency()
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void PlayEagleSound()
    {
        if (!soundIsPlayed)
        {
            if (sound && audioSource)
            {
                audioSource.volume = OptionsMenu.volumeSound;
                audioSource.PlayOneShot(sound);
            }
            soundIsPlayed = true;
        }
    }
}
