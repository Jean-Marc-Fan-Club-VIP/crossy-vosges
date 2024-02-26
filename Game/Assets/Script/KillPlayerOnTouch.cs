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
        if (collision.collider.GetComponent<Player>() != null)
        {
            StartCoroutine(DestroyPlayerAndLoadNextScene(collision.gameObject));
        }
    }

    IEnumerator DestroyPlayerAndLoadNextScene(GameObject player)
    {
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(sound.length); // Wait for the sound to finish playing
        Destroy(player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
