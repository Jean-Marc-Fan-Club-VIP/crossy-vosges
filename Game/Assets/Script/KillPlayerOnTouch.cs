using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerOnTouch : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            StartCoroutine(DestroyPlayerAndLoadNextScene(collision.gameObject));
        }
    }

    IEnumerator DestroyPlayerAndLoadNextScene(GameObject player)
    {
        Destroy(player);
        if (sound)
        {
            audioSource.PlayOneShot(sound);
            yield return new WaitForSeconds(sound.length); // Wait for the sound to finish playing
        }
    }
}