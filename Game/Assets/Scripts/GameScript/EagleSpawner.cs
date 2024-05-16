using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public GameObject player;
    public AudioClip sound;
    private AudioSource audioSource;

    public static bool isReady;
    private bool initialized = false;
    private bool soundIsPlayed = false;
    private bool hasTouchedPlayer = false;
    private bool flyingAnimation = false;
    private bool animationFinished = false;

    private const float LerpDesiredDurationMove = 1.5f;
    private float lerpElapsedTime;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        isReady = false;
        audioSource = GetComponent<AudioSource>();
        // Hide all renderers
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
    }

    void Update()
    {
        if(player != null)
        {
            if (isReady)
            {
                if(!initialized)
                {
                    initialized = true;
                    
                    PlayEagleSound();

                    // Show all renderers
                    foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = true;
                    }

                    transform.position = new Vector3(player.transform.position.x - 12f, player.transform.position.y + 8f, player.transform.position.z + 5f);
                    startPosition = transform.position;
                }
            }
            
            if(!hasTouchedPlayer)
            {
                endPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            }
            else
            {
                if (!flyingAnimation)
                {
                    // Player touched
                    flyingAnimation = true;

                    // Fly
                    startPosition = transform.position;
                    lerpElapsedTime = 0;
                    endPosition = new Vector3(transform.position.x + 6, transform.position.y + 8, transform.position.z + 10);

                    // Get main camera
                    Camera mainCamera = Camera.main;
                    // Remove follow player script
                    mainCamera.GetComponent<FollowPlayer>().enabled = false;
                }
            }

            if(flyingAnimation)
            {
                // Attach player
                player.GetComponent<Rigidbody>().useGravity = false;
                player.transform.parent = transform;
                player.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            }

            // Move
            if (startPosition != endPosition && isReady)
            {
                lerpElapsedTime += Time.deltaTime;
                var percentageComplete = lerpElapsedTime / LerpDesiredDurationMove;
                transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

                if (!hasTouchedPlayer)
                {
                    // Rotation
                    Vector3 rotation = transform.eulerAngles;
                    rotation.x = Mathf.Sin(percentageComplete * 2 * Mathf.PI) * 50;
                    rotation.z = percentageComplete * -45;
                    transform.eulerAngles = rotation;
                }
                
                if(flyingAnimation)
                {
                    // Rotation
                    Vector3 rotation = transform.eulerAngles;
                    rotation.x = percentageComplete * 50;
                    rotation.z = percentageComplete * (30 + 45) - 45;
                    transform.eulerAngles = rotation;
                }

                if (percentageComplete >= 1.0f)
                {
                    lerpElapsedTime = 0;
                    startPosition = endPosition;

                    hasTouchedPlayer = true;
                    if(flyingAnimation)
                    {
                        animationFinished = true;
                    }
                }
            }

            // Kill player
            if(animationFinished)
            {
                Destroy(player);
            }
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
